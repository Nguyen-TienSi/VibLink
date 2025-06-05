using MongoDB.Bson;
using MongoDB.Driver;
using VibLink.Data;
using VibLink.Models.Entities;

namespace VibLink.Repositories.Implementors
{
    public class FriendshipRepositoryImpl : MongoRepositoryImpl<Friendship>, IFriendshipRepository
    {
        public FriendshipRepositoryImpl(VibLinkDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Friendship>> FindByRequesterIdAsync(ObjectId requesterId)
        {
            return await _mongoCollection
                .Find(friendship => friendship.RequesterId == requesterId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Friendship>> FindByAddresseeIdAsync(ObjectId addresseeId)
        {
            return await _mongoCollection
                .Find(friendship => friendship.AddresseeId == addresseeId)
                .ToListAsync();
        }

        public async Task<Friendship> FindByRequesterIdAndAddresseeIdAsync(ObjectId requesterId, ObjectId addresseeId)
        {
            return await _mongoCollection
                .Find(friendship => friendship.RequesterId == requesterId && friendship.AddresseeId == addresseeId)
                .FirstOrDefaultAsync();
        }
    }
}
