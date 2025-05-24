using MongoDB.Bson;
using VibLink.Models.Entities;

namespace VibLink.Repositories
{
    public interface IMongoRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> AsQueryable();
        Task<TEntity?> FindByIdAsync(ObjectId id);
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task InsertOneAsync(TEntity entity);
        Task ReplaceOneAsync(ObjectId id, TEntity entity);
        Task DeleteOneAsync(ObjectId id);
    }
}
