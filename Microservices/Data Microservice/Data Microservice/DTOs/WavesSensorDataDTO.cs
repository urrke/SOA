using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Microservice.Model
{
    public class WavesSensorDataDTO
    {
        public String BeachName { get; set; }
        public float WaveHeight { get; set; }
        public float WavePeriod { get; set; }
        public float BatteryLife { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
