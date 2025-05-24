using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VibLink.Services.Internal;

namespace VibLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUserDetailsService _userDetailsService;

        public UserDetailsController(IUserDetailsService userDetailsService)
        {
            _userDetailsService = userDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserDetails()
        {
            var userDetails = await _userDetailsService.GetAllUserDetailsAsync();
            return Ok(userDetails);
        }
    }
}
