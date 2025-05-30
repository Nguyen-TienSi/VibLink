using MongoDB.Bson;
using VibLink.Models.Entities;

namespace VibLink.Repositories
{
    public interface IUserDetailsRepository : IMongoRepository<UserDetails>
    {
        IEnumerable<UserDetails> FindUserFriends(ObjectId objectId);

        IEnumerable<UserDetails> FindBlockedUsers(ObjectId objectId);
    }
}
