using Data_Microservice.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Microservice.DTOs
{
    public static class DtoMapper
    {
        public static WaterConditionsSensorData ToWaterConditionsSensorDataEntity(WaterConditionsSensorDataDTO dto) 
        {
            WaterConditionsSensorData entity = new WaterConditionsSensorData
            {
                BeachName = dto.BeachName,
                Temperature = dto.Temperature,
                Turbidity = dto.Turbidity,
                TransducerDepth = dto.TransducerDepth,
                BatteryLife = dto.BatteryLife,
                Timestamp = dto.Timestamp
            };
            return entity;
        }

        public static WavesSensorData ToWavesSensorDataEntity(WavesSensorDataDTO dto)
        {
            WavesSensorData entity = new WavesSensorData
            {
                BeachName = dto.BeachName,
                WaveHeight = dto.WaveHeight,
                WavePeriod = dto.WavePeriod,
                BatteryLife = dto.BatteryLife,
                Timestamp = dto.Timestamp
            };
            return entity;
        }
    }
}
