using AnalyticsMicroservice.Model;
using Data_Microservice.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsMicroservice.Services
{
    public class MongoDbService : IMongoDbService
    {
        public readonly IAppContext _dbContext;
        public IMongoCollection<SensorEvent> eventsCollection;
        public MongoDbService(IAppContext dbContext)
        {
            _dbContext = dbContext;
            eventsCollection = _dbContext.MongoConnection.GetCollection<SensorEvent>("sensor_events");
        }

        public void AddEvent(SensorEvent sensorEvent)
        {
            eventsCollection.InsertOne(sensorEvent);
        }

        public List<SensorEvent> GetSensorEvents()
        {
            return eventsCollection.Find(FilterDefinition<SensorEvent>.Empty).Limit(20).ToList();
        }
    }
}
