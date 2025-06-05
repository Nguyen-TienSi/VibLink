using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using VibLink.Services.Internal;

namespace VibLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;

        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpGet("requester")]
        public async Task<IActionResult> GetByRequester()
        {
            var friendships = await _friendshipService.GetByRequesterAsync();
            if (friendships == null || !friendships.Any())
            {
                return NotFound("No friendships found for the addressee.");
            }
            return Ok(friendships);
        }

        [HttpGet("addressee")]
        public async Task<IActionResult> GetByAddressee()
        {
            var friendships = await _friendshipService.GetByAddresseeAsync();
            if (friendships == null || !friendships.Any())
            {
                return NotFound("No friendships found for the addressee.");
            }
            return Ok(friendships);
        }

        [HttpGet("requester/{addresseeId}")]
        public async Task<IActionResult> GetByRequester([FromRoute] ObjectId addresseeId)
        {
            if (addresseeId == ObjectId.Empty)
            {
                return BadRequest("Addressee ID cannot be empty.");
            }
            var friendship = await _friendshipService.GetByRequesterAsync(addresseeId);
            if (friendship == null)
            {
                return NotFound("Friendship not found for the given addressee ID.");
            }
            return Ok(friendship);
        }

        [HttpGet("addressee/{requesterId}")]
        public async Task<IActionResult> GetByAddressee([FromRoute] ObjectId requesterId)
        {
            if (requesterId == ObjectId.Empty)
            {
                return BadRequest("Requester ID cannot be empty.");
            }
            var friendship = await _friendshipService.GetByAddresseeAsync(requesterId);
            if (friendship == null)
            {
                return NotFound("Friendship not found for the given requester ID.");
            }
            return Ok(friendship);
        }

        [HttpPost("{addresseeId}")]
        public async Task<IActionResult> InsertByRequester([FromRoute] ObjectId addresseeId)
        {
            if (addresseeId == ObjectId.Empty)
            {
                return BadRequest("Addressee ID cannot be empty.");
            }
            var friendship = await _friendshipService.InsertByRequesterAsync(addresseeId);
            return CreatedAtAction(nameof(GetByRequester), new { addresseeId }, friendship);
        }

        [HttpPut("requester/{addresseeId}")]
        public async Task<IActionResult> UpdateByRequester([FromRoute] ObjectId addresseeId, [FromBody] Models.DTOs.Shared.FriendRequestStatus status)
        {
            if (addresseeId == ObjectId.Empty)
            {
                return BadRequest("Addressee ID cannot be empty.");
            }
            var friendship = await _friendshipService.UpdateByRequesterAsync(addresseeId, status);
            return Ok(friendship);
        }

        [HttpPut("addressee/{requesterId}")]
        public async Task<IActionResult> UpdateByAddressee([FromRoute] ObjectId requesterId, [FromBody] Models.DTOs.Shared.FriendRequestStatus status)
        {
            if (requesterId == ObjectId.Empty)
            {
                return BadRequest("Requester ID cannot be empty.");
            }
            var friendship = await _friendshipService.UpdateByAddresseeAsync(requesterId, status);
            return Ok(friendship);
        }
    }
}
