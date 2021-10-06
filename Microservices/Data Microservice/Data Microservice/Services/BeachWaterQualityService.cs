using Data_Microservice.DTOs;
using Data_Microservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Microservice.Services
{
    public class BeachWaterQualityService : IBeachWaterQualityService
    {
        private readonly IMongoDbService _db;
        private readonly IMessageBrokerService _msgService;
        private readonly IAppContext _appContext;

        public BeachWaterQualityService(IMongoDbService db, IMessageBrokerService msgService, IAppContext appContext)
        {
            this._db = db;
            this._msgService = msgService;
            this._appContext = appContext;
        }

        public void proceedWaterConditionsSensorData(WaterConditionsSensorDataDTO data)
        {
            WaterConditionsSensorData sensorData = DtoMapper.ToWaterConditionsSensorDataEntity(data);
            this._db.AddWaterConditionsSensorData(sensorData);
            this._msgService.Publish(sensorData, this._appContext.WaterConditionsTopic);
        }

        public void proceedWavesSensorData(WavesSensorDataDTO data)
        {
            WavesSensorData sensorData = DtoMapper.ToWavesSensorDataEntity(data);
            this._db.AddWavesSensorData(sensorData);
            this._msgService.Publish(sensorData, this._appContext.WavesTopic);

        }

        public List<WaterConditionsSensorData> getWaterConditionsSensorData(string beachName)
        {
            return _db.getWaterConditionsSensorData(beachName);
        }

        public List<WavesSensorData> getWavesSensorData(string beachName)
        {
            return _db.getWavesSensorData(beachName);
        }

        public List<string> getAllBeachNames()
        {
            return _db.getAllBeachNames();
        }
    }
}
