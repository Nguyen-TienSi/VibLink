using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using VibLink.Models.DTOs.Request;
using VibLink.Services.Internal;

namespace VibLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out var messageId))
            {
                return BadRequest("Invalid ObjectId format.");
            }
            var message = await _messageService.GetById(messageId);
            if (message == null)
            {
                return NotFound("Message not found.");
            }
            return Ok(message);
        }

        [HttpGet("by-conversation/{id}")]
        public async Task<IActionResult> GetByConversationId([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out var conversationId))
            {
                return BadRequest("Invalid ObjectId format.");
            }

            var messages = await _messageService.GetByConversationId(conversationId);

            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOneAsync([FromBody] MessageCreateRequest messageCreateRequest)
        {
            if (messageCreateRequest == null)
            {
                return BadRequest("Message create request cannot be null.");
            }
            var messageDetails = await _messageService.InsertOneAsync(messageCreateRequest);
            return CreatedAtAction(nameof(GetById), new { messageDetails.Id }, messageDetails);
        }
    }
}
