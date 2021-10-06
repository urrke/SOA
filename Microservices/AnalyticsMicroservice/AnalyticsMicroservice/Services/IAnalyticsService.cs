using AnalyticsMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsMicroservice.Services
{
    public interface IAnalyticsService
    {
        void StartListening();
        void AddEvent(SensorEvent sensorEvent);

        List<SensorEvent> GetSensorEvents();
    }
}
