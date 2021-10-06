using MongoDB.Driver;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIGateway
{
    public interface IAppContext
    {
        HttpClient HttpClient { get; }
        IMqttClient MqttClient { get; }
        string WaterConditionsTopic { get; }
        string WavesTopic { get;  }
        string EventsTopic { get; }
    }
}
