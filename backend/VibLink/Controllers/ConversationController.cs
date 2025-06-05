using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;
using VibLink.Models.DTOs.Request;
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
        public async Task<IActionResult> GetByParticipant()
        {
            var conversations = await _conversationService.GetByParticipant();
            if (conversations == null || !conversations.Any())
            {
                return NotFound("No conversations found for the participant.");
            }
            return Ok(conversations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid conversation ID format.");
            }
            var conversation = await _conversationService.GetById(objectId);
            if (conversation == null)
            {
                return NotFound($"Conversation with ID {id} not found.");
            }
            return Ok(conversation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConversation([FromBody] ConversationCreateRequest conversationCreateRequest)
        {
            if (conversationCreateRequest == null)
            {
                return BadRequest("Conversation data is required.");
            }
            var createdConversation = await _conversationService.InsertOneAsync(conversationCreateRequest);
            return CreatedAtAction(nameof(GetById), new { createdConversation.Id }, createdConversation);
        }
    }
}
