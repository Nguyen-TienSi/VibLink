using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using VibLink.Models.DTOs.Response;
using VibLink.Services.Internal;

namespace VibLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpGet("by-participant")]
        public ActionResult<IEnumerable<ConversationDetailsResponse>> GetByParticipant()
        {
            var conversations = _conversationService.GetByParticipant();
            if (conversations == null || !conversations.Any())
            {
                return NotFound("No conversations found for the participant.");
            }
            return Ok(conversations);
        }

        [HttpGet("{id}")]
        public ActionResult<ConversationDetailsResponse> GetById([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid conversation ID format.");
            }
            var conversation = _conversationService.GetById(objectId);
            if (conversation == null)
            {
                return NotFound($"Conversation with ID {id} not found.");
            }
            return Ok(conversation);
        }
    }
}
