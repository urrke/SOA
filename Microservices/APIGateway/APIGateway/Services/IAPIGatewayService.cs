
using APIGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Services
{
    public interface IAPIGatewayService
    {
        void StartListening();

        List<WaterConditionsSensorData> getWaterConditionsSensorData(string beachName);
        List<WavesSensorData> getWavesSensorData(string beachName);
        List<string> getAllBeachNames();
        List<SensorEvent> GetSensorEvents();
    }
}
