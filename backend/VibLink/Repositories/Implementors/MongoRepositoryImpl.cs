using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;
using VibLink.Data;
using VibLink.Models.Entities;

namespace VibLink.Repositories.Implementors
{
    public class MongoRepositoryImpl<TEntity> : IMongoRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IMongoCollection<TEntity> _mongoCollection;

        public MongoRepositoryImpl(VibLinkDbContext dbContext)
        {
            _mongoCollection = dbContext.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            if (documentType.GetCustomAttributes(typeof(CollectionAttribute), true)?.FirstOrDefault() 
                is CollectionAttribute collectionAttribute)
            {
                return collectionAttribute.Name;
            }
            throw new ArgumentException($"Collection name not found for {documentType.Name}.");
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return _mongoCollection.AsQueryable();
        }

        public virtual async Task<TEntity?> FindByIdAsync(ObjectId id)
        {
            return await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await _mongoCollection.Find(_ => true).ToListAsync();
        }

        public virtual async Task InsertOneAsync(TEntity entity)
        {
            await _mongoCollection.InsertOneAsync(entity);
        }

        public virtual async Task ReplaceOneAsync(ObjectId id, TEntity entity)
        {
            await _mongoCollection.ReplaceOneAsync(x => x.Id == id, entity);
        }

        public virtual async Task DeleteOneAsync(ObjectId id)
        {
            await _mongoCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
