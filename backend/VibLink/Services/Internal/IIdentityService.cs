using MongoDB.Bson;
using VibLink.Models.Entities;
using VibLink.Models.Enumerations;

namespace VibLink.Services.Internal
{
    public interface IIdentityService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
        string GenerateAccessToken(ObjectId userDetailsId, UserRole userRole);
        Task<RefreshToken> GenerateRefreshToken(ObjectId userDetailsId, UserRole userRole, string? ipAddress);
        Task<RefreshToken> GenerateRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken?> RevokeRefreshTokenAsync(string refreshToken, string? ipAddress);
    }
}
