using AutoMapper;
using VibLink.Models.DTOs.Request;
using VibLink.Models.Entities;
using VibLink.Repositories;
using VibLink.Mappers;
using VibLink.Models.DTOs.Response;
using MongoDB.Bson;
using VibLink.Models.Enumerations;

namespace VibLink.Services.Internal.Implementors
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IFileStorageRepository _fileStorageRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public AuthServiceImpl(
            IUserDetailsRepository userDetailsRepository,
            IFileStorageRepository fileStorageRepository,
            IMapper mapper,
            IIdentityService identityService)
        {
            _userDetailsRepository = userDetailsRepository;
            _fileStorageRepository = fileStorageRepository;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<(bool IsSuccess, AuthTokenResponse? Tokens, string? ErrorMessage)> LoginAsync(string email, string password, string? ipAddress)
        {
            var user = await _userDetailsRepository.FindByEmailAsync(email);
            if (user == null)
                return (false, null, "User not found.");

            if (!_identityService.VerifyPassword(password, user.PasswordHash))
                return (false, null, "Invalid password.");

            var role = user.UserRoles?.FirstOrDefault() ?? UserRole.USER;
            var accessToken = _identityService.GenerateAccessToken(user.Id, role);
            var refreshToken = await _identityService.GenerateRefreshToken(user.Id, role, ipAddress);

            return (true, new AuthTokenResponse { AccessToken = accessToken, RefreshToken = refreshToken.Token }, null);
        }

        public async Task<(bool IsSuccess, AuthTokenResponse? Tokens, string? ErrorMessage)> RegisterAsync(UserRegisterRequest userRegisterRequest, string? ipAddress)
        {
            var existing = await _userDetailsRepository.FindByEmailAsync(userRegisterRequest.Email);
            if (existing != null)
                return (false, null, "Email already registered.");

            var user = _mapper.Map<UserDetails>(
                userRegisterRequest,
                opt => opt.Items["PasswordHasher"] = (Func<string, string>)_identityService.HashPassword
            );

            if (userRegisterRequest.Picture != null)
            {
                var picture = _mapper.Map<FileStorage>(userRegisterRequest.Picture);
                await _fileStorageRepository.InsertOneAsync(picture);
                user.PictureId = picture.Id;
            }

            await _userDetailsRepository.InsertOneAsync(user);

            var role = user.UserRoles?.FirstOrDefault() ?? UserRole.USER;
            var accessToken = _identityService.GenerateAccessToken(user.Id, role);
            var refreshToken = await _identityService.GenerateRefreshToken(user.Id, role, ipAddress);

            return (true, new AuthTokenResponse { AccessToken = accessToken, RefreshToken = refreshToken.Token }, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> LogoutAsync(string refreshToken, string? ipAddress)
        {
            var token = await _identityService.RevokeRefreshTokenAsync(refreshToken, ipAddress);

            if (token == null) return (false, "Invalid or expired token");

            return (true, null);
        }

        public async Task<(bool IsSuccess, AuthTokenResponse? Tokens, string? ErrorMessage)> RefreshTokenAsync(string refreshToken, string? ipAddress)
        {
            var previousRefreshToken = await _identityService.RevokeRefreshTokenAsync(refreshToken, ipAddress);
            if (previousRefreshToken == null) return (false, null, "Invalid or expired refresh token.");

            var user = await _userDetailsRepository.FindByIdAsync(previousRefreshToken.UserDetailsId);
            if (user == null) return (false, null, "User not found.");

            var role = user.UserRoles?.FirstOrDefault() ?? UserRole.USER;
            var newAccessToken = _identityService.GenerateAccessToken(user.Id, role);

            previousRefreshToken.CreatedByIp = ipAddress;
            var newRefreshToken = await _identityService.GenerateRefreshToken(previousRefreshToken);

            return (true, new AuthTokenResponse { AccessToken = newAccessToken, RefreshToken = newRefreshToken.Token }, null);
        }
    }
}
