using MongoDB.Bson;
using VibLink.Models.DTOs.Shared;

namespace VibLink.Models.DTOs.Request
{
    public record MessageCreateRequest
    {
        public string Content { get; init; } = string.Empty;

        public MessageType MessageType { get; init; }

        public string ConversationId { get; init; } = string.Empty;
    }
}
