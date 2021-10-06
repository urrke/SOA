using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Microservice.Model
{
    public class WaterConditionsSensorDataDTO
    {
        public String BeachName { get; set; }
        public float Temperature { get; set; }
        public float Turbidity { get; set; }
        public float TransducerDepth { get; set; }
        public float BatteryLife { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
