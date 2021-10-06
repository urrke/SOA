using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnalyticsMicroservice.Model
{
    public class SensorEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int eventType { get; set; }
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
