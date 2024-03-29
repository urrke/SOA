﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandMicroservice.Model
{
    public class SensorEvent
    {
        public EventType eventType { get; set; }
        public float value { get; set; }
        public string timestamp { get; set; }
        public string beachName { get; set; }
    }

    public enum EventType
    {
        HotWaterAlert,
        ColdWaterAlert,
        TurbidWaterAlert,
        ClearWaterAlert,
        LowBatteryAlert,
        FullBatteryAlert
    }
}
