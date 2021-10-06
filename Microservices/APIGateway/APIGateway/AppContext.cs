
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

namespace APIGateway
{
    public class AppContext : IAppContext
    {
        private HttpClient _httpClient;
        public HttpClient HttpClient
        {
            get
            {
                if (this._httpClient is null)
                {
                    this._httpClient = new HttpClient();
                }

                return this._httpClient;
            }
        }

        private IMqttClient _client;

        public IMqttClient MqttClient
        {
            get
            {
                if(_client == null)
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

                    }
                    catch(Exception e)
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
    }
}
