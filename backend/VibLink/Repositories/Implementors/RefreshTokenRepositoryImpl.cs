using MongoDB.Driver;
using VibLink.Data;
using VibLink.Models.Entities;

namespace VibLink.Repositories.Implementors
{
    public class RefreshTokenRepositoryImpl : MongoRepositoryImpl<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepositoryImpl(VibLinkDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<RefreshToken?> FindByTokenAsync(string refreshToken)
        {
            return await GetMongoCollection().Find(x => x.Token == refreshToken).FirstOrDefaultAsync();
        }
    }
}
