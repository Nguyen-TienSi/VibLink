using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using VibLink.Models.DTOs.Request;
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

        [HttpGet("requester/{addresseeId}")]
        public async Task<IActionResult> GetByRequester([FromRoute] string addresseeId)
        {
            if (!ObjectId.TryParse(addresseeId, out var objectId) || objectId == ObjectId.Empty)
            {
                return BadRequest("Addressee ID is invalid or empty.");
            }
            var friendship = await _friendshipService.GetByRequesterAsync(objectId);
            if (friendship == null)
            {
                return NotFound("Friendship not found for the given addressee ID.");
            }
            return Ok(friendship);
        }

        [HttpGet("addressee/{requesterId}")]
        public async Task<IActionResult> GetByAddressee([FromRoute] string requesterId)
        {
            if (!ObjectId.TryParse(requesterId, out var objectId) || objectId == ObjectId.Empty)
            {
                return BadRequest("Requester ID is invalid or empty.");
            }
            var friendship = await _friendshipService.GetByAddresseeAsync(objectId);
            if (friendship == null)
            {
                return NotFound("Friendship not found for the given requester ID.");
            }
            return Ok(friendship);
        }

        [HttpPost("{addresseeId}")]
        [Authorize]
        public async Task<IActionResult> InsertByRequester([FromRoute] string addresseeId)
        {
            if (!ObjectId.TryParse(addresseeId, out var objectId) || objectId == ObjectId.Empty)
            {
                return BadRequest("Addressee ID is invalid or empty.");
            }
            var friendship = await _friendshipService.InsertByRequesterAsync(objectId);
            return CreatedAtAction(nameof(GetByRequester), new { addresseeId }, friendship);
        }

        [HttpPut("requester/{addresseeId}")]
        public async Task<IActionResult> UpdateByRequester([FromRoute] string addresseeId, [FromBody] FriendshipRequestStatusRequest friendshipRequestStatus)
        {
            if (!ObjectId.TryParse(addresseeId, out var objectId) || objectId == ObjectId.Empty)
            {
                return BadRequest("Addressee ID is invalid or empty.");
            }
            var friendship = await _friendshipService.UpdateByRequesterAsync(objectId, friendshipRequestStatus);
            return Ok(friendship);
        }

        [HttpPut("addressee/{requesterId}")]
        public async Task<IActionResult> UpdateByAddressee([FromRoute] string requesterId, [FromBody] FriendshipRequestStatusRequest friendshipRequestStatus)
        {
            if (!ObjectId.TryParse(requesterId, out var objectId) || objectId == ObjectId.Empty)
            {
                return BadRequest("Requester ID is invalid or empty.");
            }
            var friendship = await _friendshipService.UpdateByAddresseeAsync(objectId, friendshipRequestStatus);
            return Ok(friendship);
        }

        [HttpGet("invites")]
        public async Task<IActionResult> GetAllInvites()
        {
            var invites = await _friendshipService.GetPendingInvitesAsync();
            return Ok(invites);
        }
    }
}
