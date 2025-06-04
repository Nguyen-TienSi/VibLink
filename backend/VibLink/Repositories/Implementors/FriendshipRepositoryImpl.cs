using VibLink.Data;
using VibLink.Models.Entities;

namespace VibLink.Repositories.Implementors
{
    public class FriendshipRepositoryImpl : MongoRepositoryImpl<Friendship>, IFriendshipRepository
    {
        public FriendshipRepositoryImpl(VibLinkDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Friendship> FindByAddressee(UserDetails addressee)
        {
            return [.. AsQueryable().Where(f => f.Addressee.Id == addressee.Id)];
        }
    }
}
