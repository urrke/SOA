using MongoDB.Driver;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MQTTnet.Extensions.ManagedClient;

namespace Data_Microservice
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
                    var client = new MongoClient("mongodb://mongodb:27017"); //mongodb://localhost:27017 mongodb://172.17.0.4:27017
                    _dbConnection = client.GetDatabase("dataMicroserviceDB");
                }

                return _dbConnection;
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
    }
}
