using AnalyticsMicroservice.Model;
using Data_Microservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsMicroservice.Services
{
    public interface IMongoDbService
    {
        void AddEvent(SensorEvent sensorEvent);
        List<SensorEvent> GetSensorEvents();
    }
}
