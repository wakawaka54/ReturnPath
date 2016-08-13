using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RP_Backend.Models.Sentences
{
    [BsonIgnoreExtraElements]
    public class SentenceModel
    {
        [BsonId]
        public ObjectId ID { get; set; }

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
