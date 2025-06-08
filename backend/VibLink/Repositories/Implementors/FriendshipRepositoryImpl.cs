using MongoDB.Bson;
using MongoDB.Driver;
using VibLink.Data;
using VibLink.Models.Entities;
using VibLink.Models.Enumerations;

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

        public async Task<IEnumerable<Friendship>> FindPendingByRequesterIdAsync(ObjectId requesterId)
        {
            return await _mongoCollection
                .Find(f => f.RequesterId == requesterId && f.FriendshipRequestStatus == FriendshipRequestStatus.PENDING)
                .ToListAsync();
        }

        public async Task<IEnumerable<Friendship>> FindPendingByAddresseeIdAsync(ObjectId addresseeId)
        {
            return await _mongoCollection
                .Find(f => f.AddresseeId == addresseeId && f.FriendshipRequestStatus == FriendshipRequestStatus.PENDING)
                .ToListAsync();
        }
    }
}
