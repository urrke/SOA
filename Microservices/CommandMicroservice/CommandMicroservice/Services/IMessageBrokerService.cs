using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandMicroservice.Services
{
    public interface IMessageBrokerService
    {
        void Publish(object data, string topic);
        void Subscribe(string topic);
    }
}
