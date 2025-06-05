using AutoMapper;
using VibLink.Models.DTOs.Request;
using VibLink.Models.Entities;
using VibLink.Repositories;
using VibLink.Helpers;
using VibLink.Mappers;

namespace VibLink.Services.Internal.Implementors
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IFileStorageRepository _fileStorageRepository;
        private readonly IMapper _mapper;
        private readonly AuthManager _authManager = new();

        public AuthServiceImpl(
            IUserDetailsRepository userDetailsRepository,
            IFileStorageRepository fileStorageRepository,
            IMapper mapper)
        {
            _userDetailsRepository = userDetailsRepository;
            _fileStorageRepository = fileStorageRepository;
            _mapper = mapper;
        }

        public async Task<(bool IsSuccess, string? Token, string? ErrorMessage)> LoginAsync(string email, string password)
        {
            var user = await _userDetailsRepository.FindByEmailAsync(email);
            if (user == null)
                return (false, null, "User not found.");

            if (!_authManager.VerifyPassword(password, user.PasswordHash))
                return (false, null, "Invalid password.");

            // TODO: Generate JWT token here
            var token = "dummy-jwt-token";
            return (true, token, null);
        }

        public async Task<(bool IsSuccess, string? Token, string? ErrorMessage)> RegisterAsync(UserRegisterRequest userRegisterRequest)
        {
            var existing = await _userDetailsRepository.FindByEmailAsync(userRegisterRequest.Email);
            if (existing != null)
                return (false, null, "Email already registered.");

            var user = _mapper.Map<UserDetails>(
                userRegisterRequest,
                opt => opt.Items["PasswordHasher"] = (Func<string, string>)_authManager.HashPassword
            );

            if (userRegisterRequest.Picture != null)
            {
                var picture = _mapper.Map<FileStorage>(userRegisterRequest.Picture);
                await _fileStorageRepository.InsertOneAsync(picture);
                user.PictureId = picture.Id;
            }

            _userDetailsRepository.InsertOneAsync(user).Wait();

            // TODO: Generate JWT token here
            var token = "dummy-jwt-token";
            return (true, token, null);
        }
    }
}
