using APIGateway.DTOs;
using APIGateway.Model;
using APIGateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("apigateway")]
    public class APIGatewayController : ControllerBase
    {
        IAPIGatewayService _apiService;
        public APIGatewayController(IAPIGatewayService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public string GetHelloWorld()
        {
            return "Hello world!";
        }

        [Route("beaches")]
        public List<string> getAllBeaches()
        {
            return _apiService.getAllBeachNames();
        }

        [HttpGet]
        [Route("water-conditions-sensor/{beachName}")]
        public List<WaterConditionsSensorData> getWaterConditionsSensorData(string beachName)
        {
            return _apiService.getWaterConditionsSensorData(beachName);
        }

        [HttpGet]
        [Route("waves-sensor/{beachName}")]
        public List<WavesSensorData> getWavesSensorData(string beachName)
        {
            return _apiService.getWavesSensorData(beachName);
        }

        [HttpGet]
        [Route("events")]
        public List<SensorEvent> GetSensorEvents()
        {
            return _apiService.GetSensorEvents();
        }
    }
}
