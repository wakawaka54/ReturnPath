
using MongoDB.Bson.Serialization.Attributes;

namespace RP_Backend.Models.Sentences
{
    public class StatisticsModel
    {
        [BsonElement("_id")]
        public string Tag { get; set; }

        [BsonElement("value")]
        public int Count { get; set; }
    }
}
