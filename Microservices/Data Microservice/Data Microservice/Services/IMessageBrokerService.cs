using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Microservice.Services
{
    public interface IMessageBrokerService
    {
        void Publish(object data, string topic);
    }
}
