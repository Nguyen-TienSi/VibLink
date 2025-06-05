using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
            {
                return BadRequest("Invalid login request.");
            }
            var result = await _authService.LoginAsync(request.Email, request.Password);
            if (result.IsSuccess)
            {
                return Ok(result.Token);
            }
            return Unauthorized(result.ErrorMessage);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid registration request.");
            }
            var result = await _authService.RegisterAsync(request);
            if (result.IsSuccess)
            {
                return Ok(result.Token);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok("Logged out successfully.");
        }
    }
}
