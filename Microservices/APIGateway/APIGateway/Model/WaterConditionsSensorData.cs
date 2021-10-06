using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace APIGateway.Model
{
    public class WaterConditionsSensorData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public String BeachName { get; set; }
        public float Temperature { get; set; }
        public float Turbidity { get; set; }
        public float TransducerDepth { get; set; }
        public float BatteryLife { get; set; }
        public String Timestamp { get; set; }
    }
}
