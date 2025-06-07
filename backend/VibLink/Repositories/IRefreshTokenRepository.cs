using VibLink.Models.Entities;

namespace VibLink.Repositories
{
    public interface IRefreshTokenRepository : IMongoRepository<RefreshToken>
    {
        Task<RefreshToken?> FindByTokenAsync(string refreshToken);
    }
}
