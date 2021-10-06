using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data_Microservice.Model
{
    public class WavesSensorData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public String BeachName { get; set; }
        public float WaveHeight { get; set; }
        public float WavePeriod { get; set; }
        public float BatteryLife { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
