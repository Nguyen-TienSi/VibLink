using VibLink.Models.Entities;

namespace VibLink.Repositories
{
    public interface IFriendshipRepository : IMongoRepository<Friendship>
    {
        IEnumerable<Friendship> FindByAddressee(UserDetails addressee);
    }
}
