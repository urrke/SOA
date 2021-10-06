using MongoDB.Driver;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Microservice
{
    public interface IAppContext
    {
        IMongoDatabase MongoConnection { get; }
        public IMqttClient MqttClient { get; }
        string WaterConditionsTopic { get; }
        string WavesTopic { get;  }
    }
}
