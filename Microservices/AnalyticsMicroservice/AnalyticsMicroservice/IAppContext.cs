using MongoDB.Driver;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsMicroservice
{
    public interface IAppContext
    {
        IMongoDatabase MongoConnection { get; }
        public IMqttClient MqttClient { get; }
        string WaterConditionsTopic { get; }
        string WavesTopic { get;  }
        string EventsTopic { get; }
    }
}
