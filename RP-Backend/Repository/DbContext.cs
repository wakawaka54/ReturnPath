using MongoDB.Driver;

namespace RP_Backend.Repository
{
    public class DbContext : IDbContext
    {
        IMongoDatabase _db;

        public DbContext()
        {
            //future, add paths to config
            var _client = new MongoClient("mongodb://127.0.0.1:27017");
            _db = _client.GetDatabase("RPDB");
        }

        public DbContext(string dbName)
        {
            var _client = new MongoClient("mongodb://127.0.0.1:27017");
            _db = _client.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}