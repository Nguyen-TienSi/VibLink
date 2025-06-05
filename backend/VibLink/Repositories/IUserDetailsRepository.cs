using MongoDB.Bson;
using VibLink.Models.Entities;

namespace VibLink.Repositories
{
    public interface IUserDetailsRepository : IMongoRepository<UserDetails>
    {
        Task<IEnumerable<UserDetails>> FindUserFriendsAsync(ObjectId userDetailsId);

        Task<IEnumerable<UserDetails>> FindBlockedUsersAsync(ObjectId userDetailsId);

        Task<UserDetails?> FindByEmailAsync(string email);
    }
}
