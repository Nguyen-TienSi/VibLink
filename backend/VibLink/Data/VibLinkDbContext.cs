using MongoDB.Driver;

namespace VibLink.Data
{
    public class VibLinkDbContext(IMongoDatabase database)
    {
        public IMongoCollection<T> GetCollection<T>(string collectionName) =>
            database.GetCollection<T>(collectionName);
    }
}
