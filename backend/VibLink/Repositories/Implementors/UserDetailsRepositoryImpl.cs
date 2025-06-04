using MongoDB.Bson;
using VibLink.Data;
using VibLink.Models.Entities;

namespace VibLink.Repositories.Implementors
{
    public class UserDetailsRepositoryImpl : MongoRepositoryImpl<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepositoryImpl(VibLinkDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<UserDetails> FindUserFriends(ObjectId objectId)
        {
            var user = this.FindByIdAsync(objectId).Result;
            return user?.Friends ?? Enumerable.Empty<UserDetails>();
        }

        public IEnumerable<UserDetails> FindBlockedUsers(ObjectId objectId)
        {
            var user = this.FindByIdAsync(objectId).Result;
            return user?.BlockedUsers ?? Enumerable.Empty<UserDetails>();
        }

        public UserDetails? FindByEmail(string email)
        {
            return AsQueryable().FirstOrDefault(u => u.Email == email);
        }
    }
}
