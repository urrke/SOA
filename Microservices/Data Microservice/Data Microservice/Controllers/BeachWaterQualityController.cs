using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Microservice.Model;
using Data_Microservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Data_Microservice.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class BeachWaterQualityController : ControllerBase
    {
        private readonly IBeachWaterQualityService _bwqService;

        public BeachWaterQualityController(IBeachWaterQualityService bwqService)
        {
            this._bwqService = bwqService;
        }

        [HttpGet]
        public String getHello()
        {
            //this.msg.Publish(new object(), "topic");
            return "Hello world";
        }

        [HttpGet]
        [Route("beaches")]
        public List<string> getAllBeaches()
        {
            return _bwqService.getAllBeachNames();
        }

        [HttpGet]
        [Route("water-conditions-sensor/{beachName}")]
        public List<WaterConditionsSensorData> getWaterConditionsSensorData(string beachName)
        {
            return _bwqService.getWaterConditionsSensorData(beachName);
        }

        [HttpGet]
        [Route("waves-sensor/{beachName}")]
        public List<WavesSensorData> getWavesSensorData(string beachName)
        {
            return _bwqService.getWavesSensorData(beachName);
        }

        [HttpPost]
        [Route("water-conditions-sensor")]
        public void proceedWaterConditionsSensorData([FromBody]WaterConditionsSensorDataDTO data)
        {
            _bwqService.proceedWaterConditionsSensorData(data);
        }

        [HttpPost]
        [Route("waves-sensor")]
        public void proceedWavesSensorData([FromBody]WavesSensorDataDTO data)
        {
            _bwqService.proceedWavesSensorData(data);
        }
    }
}