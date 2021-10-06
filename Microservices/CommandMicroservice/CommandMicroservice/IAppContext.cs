using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandMicroservice
{
    public interface IAppContext
    {
        public IMqttClient MqttClient { get; }
        string WaterConditionsTopic { get; }
        string WavesTopic { get;  }
        string EventsTopic { get; }
        string CommandsTopic { get; }
        string WaterConditionsSensorAddres { get; }
        string WavesSensorAddres { get; }
    }
}
