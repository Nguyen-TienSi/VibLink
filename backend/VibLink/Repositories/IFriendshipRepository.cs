using MongoDB.Bson;
using VibLink.Models.Entities;

namespace VibLink.Repositories
{
    public interface IFriendshipRepository : IMongoRepository<Friendship>
    {
        Task<IEnumerable<Friendship>> FindByRequesterIdAsync(ObjectId requesterId);

        Task<IEnumerable<Friendship>> FindByAddresseeIdAsync(ObjectId addresseeId);

        Task<Friendship> FindByRequesterIdAndAddresseeIdAsync(ObjectId requesterId, ObjectId addresseeId);
    }
}
