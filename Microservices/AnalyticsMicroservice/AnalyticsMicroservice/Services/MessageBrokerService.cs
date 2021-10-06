using MongoDB.Bson.IO;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using MQTTnet.Client.Subscribing;
using MQTTnet.Client;

namespace AnalyticsMicroservice.Services
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

            _appContext.MqttClient.PublishAsync(message, CancellationToken.None);
        }

        public void Subscribe(string topic)
        {
            _appContext.MqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build()).Wait();
        }
    }
}
