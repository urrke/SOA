using AnalyticsMicroservice.Model;
using Data_Microservice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsMicroservice.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IMongoDbService _db;
        private readonly IMessageBrokerService _msgService;
        private readonly IAppContext _appContext;
        public AnalyticsService(IMongoDbService db, IMessageBrokerService msgService, IAppContext appContext)
        {
            this._db = db;
            this._msgService = msgService;
            this._appContext = appContext;
        }

        public void AddEvent(SensorEvent sensorEvent)
        {
            _db.AddEvent(sensorEvent);
            _msgService.Publish(sensorEvent, _appContext.EventsTopic);
        }

        public void StartListening()
        {
            this._msgService.Subscribe(this._appContext.WaterConditionsTopic);
            this._msgService.Subscribe(this._appContext.WavesTopic);
        }

        public List<SensorEvent> GetSensorEvents()
        {
            return _db.GetSensorEvents();
        }
    }
}
