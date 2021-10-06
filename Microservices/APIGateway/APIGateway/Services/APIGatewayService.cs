
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Model;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using System.Text;
using MQTTnet.Client;
using MQTTnet;

namespace APIGateway.Services
{
    public class APIGatewayService : IAPIGatewayService
    {
        private readonly IMessageBrokerService _msgService;
        private readonly IAppContext _appContext;
        private readonly IHubContext<MessageHub> _hubContext;
        private string dataMicroserviceBaseUrl = "http://data-microservice:80/api/data"; //"http://localhost:58332/api/data"
        private string analyticsMicroserviceBaseUrl = "http://analytics-microservice:80/analytics"; //"http://localhost:40014/analytics"
        public APIGatewayService(IMessageBrokerService msgService, IAppContext appContext, IHubContext<MessageHub> hubContext) 
        {
            this._msgService = msgService;
            this._appContext = appContext;
            this._hubContext = hubContext;
        }

        public void StartListening()
        {
            this._msgService.Subscribe(this._appContext.EventsTopic);

            this._appContext.MqttClient.UseApplicationMessageReceivedHandler((args) =>
            {
                this.ApplicationMessageReceivedHandler(args);
            });
        }

        public List<WaterConditionsSensorData> getWaterConditionsSensorData(string beachName)
        {
            var url = dataMicroserviceBaseUrl + "/water-conditions-sensor/" + beachName;
            var response = _appContext.HttpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<List<WaterConditionsSensorData>>(response);
        }

        public List<WavesSensorData> getWavesSensorData(string beachName)
        {
            var url = dataMicroserviceBaseUrl + "/waves-sensor/" + beachName;
            var response = _appContext.HttpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<List<WavesSensorData>>(response);
        }

        public List<string> getAllBeachNames()
        {
            var url = dataMicroserviceBaseUrl + "/beaches";
            var response = _appContext.HttpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<List<string>>(response);
        }

        public List<SensorEvent> GetSensorEvents()
        {
            var url = analyticsMicroserviceBaseUrl + "/events";
            var response = _appContext.HttpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<List<SensorEvent>>(response);
        }

        private void ApplicationMessageReceivedHandler(MqttApplicationMessageReceivedEventArgs args)
        {
            var message = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
            Console.WriteLine(message);


            if (args.ApplicationMessage.Topic.Equals(_appContext.EventsTopic))
            {
                SensorEvent eventData = System.Text.Json.JsonSerializer.Deserialize<SensorEvent>(message);
                //_hubContext.Clients.Group("events").SendAsync("new_event", eventData).Wait();
                _hubContext.Clients.All.SendAsync("new_event", eventData).Wait();
            }

        }
    }
}
