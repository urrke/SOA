using MongoDB.Bson.IO;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using MQTTnet.Client.Options;

namespace Data_Microservice.Services
{
    public class MessageBrokerService : IMessageBrokerService
    {
        private readonly IAppContext _appContext;
        public MessageBrokerService(IAppContext appContext)
        {
            this._appContext = appContext;
        }
        public void Publish(object data, string topic)
        {
            MqttApplicationMessage message = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(Newtonsoft.Json.JsonConvert.SerializeObject(data))
                    .WithRetainFlag()
                    .Build();

            if(_appContext.MqttClient.IsConnected)
                _appContext.MqttClient.PublishAsync(message, CancellationToken.None);

            /*_appContext.MqttClient.DisconnectAsync(new MqttClientOptionsBuilder()
                                .WithTcpServer("localhost", 1883) //localhost  172.17.0.3
                                .WithCleanSession()
                                .WithSessionExpiryInterval(1000000000)
                                .Build()).Wait();*/
        }
    }
}
