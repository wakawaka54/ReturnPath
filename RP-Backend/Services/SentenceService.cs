using MongoDB.Bson;
using MongoDB.Driver;
using RP_Backend.Models.Pagination;
using RP_Backend.Models.Sentences;
using RP_Backend.Repository;
using RP_Backend.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RP_Backend.Services
{
    public class SentenceService : ISentenceService
    {
        public const int ItemsPerPage = 20;

        //future, add boring words to database or config to allow modification without rebuild
        static string[] boringWords = new string[]
        {
            "to", "the", "of", "from", "how", "and", "that", "in", "for", "its", "the",
            "is", "by", "with", "on", "in", "it", "were", "be", "into", "their", "would",
            "been", "this", "including", "such", "over", "under", "or", "but", "not",
            "on", "sold", "use", "up", "some", "who", "he", "held", "its", "which",
            "billion", "other", "first", "united", "one", "will", "two", "more",
            "during", "three", "four", "five", "six", "seven", "eight", "now",
            "founded", "group", "now", "most", "us", "out", "per", "when", "led", "using",
            "while", "site", "time", "top", "buy", "there", "those", "moved", "through",
            "they", "used", "since", "between"
        };

        const string collectionName = "Sentences";

        IMongoCollection<SentenceModel> _dataStore;

		public SentenceService(IDbContext context)
		{
            _dataStore = context.GetCollection<SentenceModel>(collectionName);
        }

        public async Task<IEnumerable<StatisticsModel>> Statistics()
        {
            BsonJavaScript map = new BsonJavaScript(
                @"function() {
                for(var idx = 0; idx < this.Tags.length; idx++)
                {
                    var key = this.Tags[idx];
                    var value = 1;
                    emit(key, value);
                }
                }");

            BsonJavaScript reduce = new BsonJavaScript(
                @"function(word, values) {
                    return Array.sum(values);
                }");

            var stats = await _dataStore.MapReduce<StatisticsModel>(map, reduce).ToListAsync();

            return stats.OrderByDescending(x => x.Count).Take(15);
        }

        public async Task<SentencesGetReturnType> Get(int? page)
    	{
            if (page == null) { page = 0; }
            int p = (int)page;

            var results = await _dataStore.Find(x => true).Skip(page * ItemsPerPage).Limit(ItemsPerPage).ToListAsync<SentenceModel>();
            var count = await _dataStore.Find(x => true).CountAsync();

            var pag = new Pagination() { Current = p, Total = (int)Math.Ceiling((double)count/(double)ItemsPerPage) };

            return new SentencesGetReturnType() { Sentences = results, Pagination = pag };
        }

    	public async Task<SentenceModel> Create(SentenceModel model)
    	{
            if(model == null) { throw new ArgumentNullException("model", "Model cannot be null"); }
            if (model.Sentence.Length < 1) { throw new InvalidOperationException("Model Sentence cannot be blank"); }


            string[] split = model.Sentence.Split(' ');
    		List<string> valid = new List<string>();

    		foreach(string s in split)
    		{
                string ss = s.Trim().TrimPunctuation();

                if(ss.IsAlpha() && !isIgnoreWord(ss))
    			{
    				valid.Add(ss);
    			}
    		}

            model.Tags = valid.ToArray();

            await _dataStore.InsertOneAsync(model);

            return model;
        }

		public async Task<bool> Delete(string id)
		{
            var builder = Builders<SentenceModel>.Filter;
            var filter = builder.Eq("_id", id);

            var delete = await _dataStore.DeleteOneAsync(a => a.ID == new ObjectId(id));

            return delete.IsAcknowledged && delete.DeletedCount > 0;
        }

		bool isIgnoreWord(string s)
		{
            return boringWords.Contains(s);
        }
    }
}
