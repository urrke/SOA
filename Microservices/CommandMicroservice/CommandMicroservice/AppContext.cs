
using CommandMicroservice.DTOs;
using CommandMicroservice.Model;
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

namespace CommandMicroservice
{
    public class AppContext : IAppContext
    {

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

        private string _commandsTopic = "commands_topic";
        public string CommandsTopic { 
            get { return _commandsTopic; } 
        }

        private string _waterConditionsSensorAddres = "http://water-conditions-sensor:5000/apply-command";
        public string WaterConditionsSensorAddres
        {
            get { return _waterConditionsSensorAddres; }
        }

        private string _wavesSensorAddres = "http://waves-sensor:5000/apply-command";
        public string WavesSensorAddres
        {
            get { return _wavesSensorAddres; }
        }

        private void ApplicationMessageReceivedHandler(MqttApplicationMessageReceivedEventArgs args)
        {
            HttpClient httpClient = new HttpClient();
            var message = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
            Console.WriteLine(message);
            SensorEvent sensorEvent = System.Text.Json.JsonSerializer.Deserialize<SensorEvent>(message);

            Command commandToSend = new Command();
            commandToSend.BeachName = sensorEvent.beachName;
            commandToSend.Event = sensorEvent.eventType;
            switch (sensorEvent.eventType)
            {
                case EventType.HotWaterAlert:
                    commandToSend.CommandMessage = "Turn off heater";
                    break;
                case EventType.ColdWaterAlert:
                    commandToSend.CommandMessage = "Turn on heater";
                    break;
                case EventType.TurbidWaterAlert:
                    commandToSend.CommandMessage = "Turn on filter pump";
                    break;
                case EventType.ClearWaterAlert:
                    commandToSend.CommandMessage = "Turn off filter pump";
                    break;
                case EventType.LowBatteryAlert:
                    commandToSend.CommandMessage = "Turn on battery charger";
                    break;
                case EventType.FullBatteryAlert:
                    commandToSend.CommandMessage = "Turn off battery charger";
                    break;
            }

            Console.WriteLine("Sending command: " + commandToSend.CommandMessage + "(" + sensorEvent.beachName + ")"); 
            if (commandToSend.Event == EventType.LowBatteryAlert || commandToSend.Event == EventType.FullBatteryAlert)
            {
                var url = WavesSensorAddres + CreateUrlFromCommand(commandToSend);
                httpClient.PutAsync(url, null).Wait();
            }
            else
            {
                var url = WaterConditionsSensorAddres + CreateUrlFromCommand(commandToSend);
                httpClient.PutAsync(url, null).Wait();
            }

        }

        private string CreateUrlFromCommand(Command commandToSend)
        {
            return "?BeachName='" + commandToSend.BeachName + "'&CommandMessage='" + commandToSend.CommandMessage + "'";
        }
    }
}
