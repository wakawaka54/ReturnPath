using MongoDB.Driver;

namespace RP_Backend.Repository
{
    public class DbContext : IDbContext
    {
        //future, use IOptions to load in this configuration
        public static string MongoUrl { get; set; }

        IMongoDatabase _db;

        public DbContext()
        {
            //future, add paths to config
            var _client = new MongoClient(MongoUrl);
            _db = _client.GetDatabase("RPDB");
        }

        public DbContext(string dbName)
        {
            var _client = new MongoClient(MongoUrl);
            _db = _client.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}