using Data_Microservice.Model;
using MongoDB.Driver;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnalyticsMicroservice
{
    public class AppContext : IAppContext
    {
        private IMongoDatabase _dbConnection;

        public IMongoDatabase MongoConnection
        {
            get
            {
                if(this._dbConnection == null)
                {
                    var client = new MongoClient("mongodb://mongodb:27017");  //mongodb://localhost:27017 mongodb://172.17.0.4:27017
                    _dbConnection = client.GetDatabase("analyticsMicroserviceDB");
                }

                return _dbConnection;
            }
        }

        private IMqttClient _client;

        public IMqttClient MqttClient
        {
            get
            {
                if (_client == null)
                {
                    try
                    {
                        TimeSpan interval = new TimeSpan(0, 1, 0);
                        _client = (new MqttFactory()).CreateMqttClient();
                        _client.ConnectAsync(
                            new MqttClientOptionsBuilder()
                                .WithTcpServer("hivemq", 1883) //localhost  172.17.0.3
                                .WithKeepAlivePeriod(interval)
                                .WithCleanSession(true)
                                .Build()).Wait();

                        _client.UseApplicationMessageReceivedHandler((args) =>
                        {
                            this.ApplicationMessageReceivedHandler(args);
                        });

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            
                if (!_client.IsConnected)
                    _client.ReconnectAsync().Wait();
                return _client;
            }
        }

        private string _waterConditionsTopic = "water_conditions_topic";
        public string WaterConditionsTopic 
        { 
            get { return _waterConditionsTopic; }
        }

        private string _wavesTopic = "waves_topic";
        public string WavesTopic 
        { 
            get { return _wavesTopic; }
        }

        private string _eventsTopic = "events_topic";
        public string EventsTopic { 
            get { return _eventsTopic; }
        }

        private void ApplicationMessageReceivedHandler(MqttApplicationMessageReceivedEventArgs args)
        {
            HttpClient httpClient = new HttpClient();
            var message = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
            Console.WriteLine(message);
            if(args.ApplicationMessage.Topic.Equals(WaterConditionsTopic))
            {
                var sensorData = System.Text.Json.JsonSerializer.Deserialize<WaterConditionsSensorData>(message);
                httpClient.PostAsJsonAsync("http://siddhiio:8006/siddhi/water-condition", sensorData).Wait(); //http://localhost:8006 http://172.17.0.2:8006
            }

            if (args.ApplicationMessage.Topic.Equals(WavesTopic))
            {
                var sensorData = System.Text.Json.JsonSerializer.Deserialize<WavesSensorData>(message);
                httpClient.PostAsJsonAsync("http://siddhiio:8006/siddhi/waves", sensorData).Wait(); //http://localhost:8006
            }

        }
    }
}
