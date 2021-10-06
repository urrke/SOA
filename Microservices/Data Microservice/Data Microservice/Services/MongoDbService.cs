using Data_Microservice.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Microservice.Services
{
    public class MongoDbService : IMongoDbService
    {
        private readonly IAppContext _dbContext;
        private IMongoCollection<WaterConditionsSensorData> waterConditionsCollection;
        private IMongoCollection<WavesSensorData> wavesCollection;

        public MongoDbService(IAppContext dbContext)
        {
            this._dbContext = dbContext;
            waterConditionsCollection = _dbContext.MongoConnection.GetCollection<WaterConditionsSensorData>("water_conditions_data");
            wavesCollection = _dbContext.MongoConnection.GetCollection<WavesSensorData>("waves_data");
        }

        public void AddWaterConditionsSensorData(WaterConditionsSensorData data)
        {
            waterConditionsCollection.InsertOne(data);
        }

        public void AddWavesSensorData(WavesSensorData data)
        {
            wavesCollection.InsertOne(data);
        }

        public List<WaterConditionsSensorData> getWaterConditionsSensorData(string beachName)
        {
            FilterDefinition<WaterConditionsSensorData> filter = Builders<WaterConditionsSensorData>.Filter.Eq(x => x.BeachName, beachName);
            return waterConditionsCollection.Find(filter).SortByDescending(x => x.Timestamp).Limit(20).ToList();
        }

        public List<WavesSensorData> getWavesSensorData(string beachName)
        {
            FilterDefinition<WavesSensorData> filter = Builders<WavesSensorData>.Filter.Eq(x => x.BeachName, beachName);
            return wavesCollection.Find(filter).SortByDescending(x => x.Timestamp).Limit(20).ToList();
        }

        public List<string> getAllBeachNames()
        {

            List<string> beachNamesWCSensor = waterConditionsCollection.Distinct<string>("BeachName", FilterDefinition<WaterConditionsSensorData>.Empty).ToList();
            List<string> beachNamesWavesSensor = wavesCollection.Distinct<string>("BeachName", FilterDefinition<WavesSensorData>.Empty).ToList();

            return beachNamesWCSensor.Union(beachNamesWavesSensor).ToList();
        }
    }
}
