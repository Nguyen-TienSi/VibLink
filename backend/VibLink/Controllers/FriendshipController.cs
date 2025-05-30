using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VibLink.Models.DTOs.Response;
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

        [HttpGet("addressee")]
        public ActionResult<IEnumerable<FriendshipDetailsDto>> GetByAddressee()
        {
            var friendships = _friendshipService.GetByAddressee();
            if (friendships == null || !friendships.Any())
            {
                return NotFound("No friendships found for the addressee.");
            }
            return Ok(friendships);
        }
    }
}
