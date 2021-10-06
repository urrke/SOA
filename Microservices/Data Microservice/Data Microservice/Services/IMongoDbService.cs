using Data_Microservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Microservice.Services
{
    public interface IMongoDbService
    {
        void AddWaterConditionsSensorData(WaterConditionsSensorData data);
        void AddWavesSensorData(WavesSensorData data);
        List<WaterConditionsSensorData> getWaterConditionsSensorData(string beachName);
        List<WavesSensorData> getWavesSensorData(string beachName);

        List<string> getAllBeachNames();
    }
}
