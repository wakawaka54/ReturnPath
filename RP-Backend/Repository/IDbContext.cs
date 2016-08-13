using MongoDB.Driver;

namespace RP_Backend.Repository
{
    public interface IDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
