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

        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email cannot be null or empty.");
            }
            var user = await _userDetailsService.GetByEmail(email);
            if (user == null)
            {
                return NotFound($"User with email {email} not found.");
            }
            return Ok(user);
        }

        [HttpGet("friends")]
        public async Task<IActionResult> GetUserFriends()
        {
            var userFriends = await _userDetailsService.GetUserFriends();
            if (userFriends == null || !userFriends.Any())
            {
                return NotFound("No friends found for the user.");
            }
            return Ok(userFriends);
        }

        [HttpGet("blocked")]
        public async Task<IActionResult> GetBlockedUsers()
        {
            var blockedUsers = await _userDetailsService.GetBlockedUsers();
            if (blockedUsers == null || !blockedUsers.Any())
            {
                return NotFound("No blocked users found.");
            }
            return Ok(blockedUsers);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetUserDetails()
        {
            var userDetails = await _userDetailsService.GetUserDetails();
            if (userDetails == null)
            {
                return NotFound("User details not found.");
            }
            return Ok(userDetails);
        }

        [HttpPatch("details")]
        public async Task<IActionResult> PatchUserDetails([FromBody] JsonPatchDocument<UserDetails> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var patchedUserDetails = await _userDetailsService.PatchUserDetails(patchDocument);

            return Ok(patchedUserDetails);
        }

        [HttpGet("by-conversation/{id}")]
        public async Task<IActionResult> GetByConversationId(string id)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out var conversationId))
                return BadRequest("Invalid conversation ID.");

            var users = await _userDetailsService.GetByConversationId(conversationId);
            if (users == null || !users.Any())
                return NotFound("No users found for this conversation.");

            return Ok(users);
        }
    }
}
