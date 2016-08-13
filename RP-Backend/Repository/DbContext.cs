using MongoDB.Driver;

namespace RP_Backend.Repository
{
    public class DbContext : IDbContext
    {
        //future, use IOptions to load in this configuration
        public static string MongoUrl { get; set; }

        static string defaultUrl = "mongodb://127.0.0.1:27017";

        IMongoDatabase _db;

        public DbContext()
        {
            if(MongoUrl == null)
            {
                MongoUrl = defaultUrl;
            }

            //future, add paths to config
            var _client = new MongoClient(MongoUrl);
            _db = _client.GetDatabase("RPDB");
        }

        public DbContext(string dbName)
        {
            if (MongoUrl == null)
            {
                MongoUrl = defaultUrl;
            }

            var _client = new MongoClient(MongoUrl);
            _db = _client.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}