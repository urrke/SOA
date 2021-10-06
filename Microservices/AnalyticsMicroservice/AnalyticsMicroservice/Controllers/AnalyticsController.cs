using AnalyticsMicroservice.DTOs;
using AnalyticsMicroservice.Model;
using AnalyticsMicroservice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsMicroservice.Controllers
{
    [ApiController]
    [Route("analytics")]
    public class AnalyticsController : ControllerBase
    {
        IAnalyticsService _analyticsService;
        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet]
        public string GetHelloWorld()
        {
            return "Hello world!";
        }

        [HttpGet]
        [Route("events")]
        public List<SensorEvent> GetSensorEvents()
        {
            return _analyticsService.GetSensorEvents();
        }

        [HttpPost]
        [Route("add-event")]
        public void AddEvent([FromBody] EventDTO Event)
        {
            _analyticsService.AddEvent(Event.Event);
        }
    }
}
