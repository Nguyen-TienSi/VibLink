using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VibLink.Models.Entities;
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
        public IActionResult GetUserFriends()
        {
            var userFriends = _userDetailsService.GetUserFriends();
            if (userFriends == null || !userFriends.Any())
            {
                return NotFound("No friends found for the user.");
            }
            return Ok(userFriends);
        }

        [HttpGet("blocked")]
        public IActionResult GetBlockedUsers()
        {
            var blockedUsers = _userDetailsService.GetBlockedUsers();
            if (blockedUsers == null || !blockedUsers.Any())
            {
                return NotFound("No blocked users found.");
            }
            return Ok(blockedUsers);
        }

        [HttpGet("details")]
        public IActionResult GetUserDetails()
        {
            var userDetails = _userDetailsService.GetUserDetails();
            if (userDetails == null)
            {
                return NotFound("User details not found.");
            }
            return Ok(userDetails);
        }

        [HttpPatch("details")]
        public IActionResult PatchUserDetails([FromBody] JsonPatchDocument<UserDetails> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var patchedUserDetails = _userDetailsService.PatchUserDetails(patchDocument);

            return Ok(patchedUserDetails);
        }
    }
}
