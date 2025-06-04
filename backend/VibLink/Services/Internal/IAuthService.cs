using VibLink.Models.DTOs.Request;

namespace VibLink.Services.Internal
{
    public interface IAuthService
    {
        (bool IsSuccess, string? Token, string? ErrorMessage) Login(string email, string password);
        (bool IsSuccess, string? Token, string? ErrorMessage) Register(UserRegisterRequest userRegisterRequest);
    }
}
