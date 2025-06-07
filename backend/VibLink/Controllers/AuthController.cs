using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using VibLink.Models.DTOs.Request;
using VibLink.Services.Internal;

namespace VibLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Invalid login request.");

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await _authService.LoginAsync(request.Email, request.Password, ipAddress);
            if (result.IsSuccess)
                return Ok(result.Tokens);

            return Unauthorized(result.ErrorMessage);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Invalid registration request.");

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await _authService.RegisterAsync(request, ipAddress);
            if (result.IsSuccess)
                return Ok(result.Tokens);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.RefreshToken))
                return BadRequest("Invalid logout request.");

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await _authService.LogoutAsync(request.RefreshToken, ipAddress);
            if (result.IsSuccess)
                return Ok("Logged out successfully.");

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.RefreshToken))
                return BadRequest("Invalid refresh token request.");

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await _authService.RefreshTokenAsync(request.RefreshToken, ipAddress);
            if (result.IsSuccess)
                return Ok(result.Tokens);

            return Unauthorized(result.ErrorMessage);
        }
    }
}
