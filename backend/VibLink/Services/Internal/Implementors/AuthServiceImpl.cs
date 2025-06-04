using AutoMapper;
using VibLink.Models.DTOs.Request;
using VibLink.Models.Entities;
using VibLink.Repositories;
using VibLink.Helpers;

namespace VibLink.Services.Internal.Implementors
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IMapper _mapper;
        private readonly AuthManager _authManager = new();

        public AuthServiceImpl(IUserDetailsRepository userDetailsRepository, IMapper mapper)
        {
            _userDetailsRepository = userDetailsRepository;
            _mapper = mapper;
        }

        public (bool IsSuccess, string? Token, string? ErrorMessage) Login(string email, string password)
        {
            var user = _userDetailsRepository.FindByEmail(email);
            if (user == null)
                return (false, null, "User not found.");

            if (!_authManager.VerifyPassword(password, user.PasswordHash))
                return (false, null, "Invalid password.");

            // TODO: Generate JWT token here
            var token = "dummy-jwt-token";
            return (true, token, null);
        }

        public (bool IsSuccess, string? Token, string? ErrorMessage) Register(UserRegisterRequest userRegisterRequest)
        {
            var existing = _userDetailsRepository.FindByEmail(userRegisterRequest.Email);
            if (existing != null)
                return (false, null, "Email already registered.");

            var user = _mapper.Map<UserDetails>(
                userRegisterRequest,
                opt => opt.Items["PasswordHasher"] = (Func<string, string>)_authManager.HashPassword
            );

            _userDetailsRepository.InsertOneAsync(user).Wait();

            // TODO: Generate JWT token here
            var token = "dummy-jwt-token";
            return (true, token, null);
        }
    }
}
