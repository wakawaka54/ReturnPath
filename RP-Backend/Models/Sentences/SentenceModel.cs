using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RP_Backend.Models.Sentences
{
    [BsonIgnoreExtraElements]
    public class SentenceModel
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId ID { get; set; }

        [JsonProperty("id")]
        public string StringID
        {
            get
            {
                if(ID != null) { return ID.ToString(); }
                else { return ""; }
            }
        }

        [Required]
        [StringLength(10000, MinimumLength = 1)]
        public string Sentence { get; set; }

        public string[] Tags { get; set; }
    }

    public class SentencesGetReturnType
    {
        public Pagination.Pagination Pagination { get; set; }
        public IEnumerable<SentenceModel> Sentences { get; set; }
    }
}
