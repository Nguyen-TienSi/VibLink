using MongoDB.Bson;
using MongoDB.Driver;
using VibLink.Data;
using VibLink.Models.Entities;

namespace VibLink.Repositories.Implementors
{
    public class UserDetailsRepositoryImpl : MongoRepositoryImpl<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepositoryImpl(VibLinkDbContext dbContext) : base(dbContext) { }

        public async Task<UserDetails?> FindByEmailAsync(string email)
        {
            return await _mongoCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserDetails>> FindUserFriendsAsync(ObjectId userDetailsId)
        {
            var user = await _mongoCollection.Find(x => x.Id == userDetailsId).FirstOrDefaultAsync();
            if (user == null || user.FriendIds == null || user.FriendIds.Count == 0)
                return [];

            var friends = await _mongoCollection.Find(x => user.FriendIds.Contains(x.Id)).ToListAsync();
            return friends;
        }

        public async Task<IEnumerable<UserDetails>> FindBlockedUsersAsync(ObjectId userDetailsId)
        {
            var user = await _mongoCollection.Find(x => x.Id == userDetailsId).FirstOrDefaultAsync();
            if (user == null || user.BlockedUserIds == null || user.BlockedUserIds.Count == 0)
                return [];

            var blockedUsers = await _mongoCollection.Find(x => user.BlockedUserIds.Contains(x.Id)).ToListAsync();
            return blockedUsers;
        }
    }
}
