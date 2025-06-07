using VibLink.Models.DTOs.Request;
using VibLink.Models.DTOs.Response;

namespace VibLink.Services.Internal
{
    public interface IAuthService
    {
        Task<(bool IsSuccess, AuthTokenResponse? Tokens, string? ErrorMessage)> LoginAsync(string email, string password, string? ipAddress);
        Task<(bool IsSuccess, AuthTokenResponse? Tokens, string? ErrorMessage)> RegisterAsync(UserRegisterRequest userRegisterRequest, string? ipAddress);
        Task<(bool IsSuccess, string? ErrorMessage)> LogoutAsync(string refreshToken, string? ipAddress);
        Task<(bool IsSuccess, AuthTokenResponse? Tokens, string? ErrorMessage)> RefreshTokenAsync(string refreshToken, string? ipAddress);
    }
}
