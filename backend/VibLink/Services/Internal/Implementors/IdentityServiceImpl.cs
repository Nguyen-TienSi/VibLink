using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VibLink.Models.Configurations;
using VibLink.Models.Entities;
using VibLink.Models.Enumerations;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class IdentityServiceImpl : IIdentityService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;

        public IdentityServiceImpl(IRefreshTokenRepository refreshTokenRepository, IConfiguration configuration)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
        }

        public string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }

        public string GenerateAccessToken(ObjectId userDetailsId, UserRole userRole)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtConfig = _configuration.GetRequiredSection(nameof(JwtConfig)).Get<JwtConfig>()
                ?? throw new InvalidOperationException("JWT configuration is missing or invalid.");

            var key = Encoding.UTF8.GetBytes(jwtConfig.Secret);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userDetailsId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userDetailsId.ToString()),
                new Claim(ClaimTypes.Role, userRole.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtConfig.Issuer,
                audience: jwtConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtConfig.AccessTokenExpirationMinutes),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            return tokenHandler.WriteToken(token);
        }

        public async Task<RefreshToken> GenerateRefreshToken(ObjectId userDetailsId, UserRole userRole, string? ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                Expires = DateTime.UtcNow.AddDays(7),
                CreatedByIp = ipAddress,
                UserDetailsId = userDetailsId
            };

            await _refreshTokenRepository.InsertOneAsync(refreshToken);

            return refreshToken;
        }

        public async Task<RefreshToken> GenerateRefreshToken(RefreshToken refreshToken)
        {
            var newToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                Expires = refreshToken.Expires,
                CreatedByIp = refreshToken.CreatedByIp,
                UserDetailsId = refreshToken.UserDetailsId
            };

            await _refreshTokenRepository.InsertOneAsync(newToken);

            return newToken;
        }

        public async Task<RefreshToken?> RevokeRefreshTokenAsync(string refreshToken, string? ipAddress)
        {
            var token = await _refreshTokenRepository.FindByTokenAsync(refreshToken);
            if (token == null || !token.IsActive) return null;

            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            await _refreshTokenRepository.ReplaceOneAsync(token.Id, token);

            return token;
        }

        private string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
