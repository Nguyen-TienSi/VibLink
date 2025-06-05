using VibLink.Models.DTOs.Request;

namespace VibLink.Services.Internal
{
    public interface IAuthService
    {
        Task<(bool IsSuccess, string? Token, string? ErrorMessage)> LoginAsync(string email, string password);
        Task<(bool IsSuccess, string? Token, string? ErrorMessage)> RegisterAsync(UserRegisterRequest userRegisterRequest);
    }
}
