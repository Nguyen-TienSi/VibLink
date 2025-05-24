using VibLink.Data;
using VibLink.Models.Entities;

namespace VibLink.Repositories.Implementors
{
    public class UserDetailsRepositoryImpl : MongoRepositoryImpl<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepositoryImpl(VibLinkDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
