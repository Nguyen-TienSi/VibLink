using MongoDB.Bson;
using VibLink.Models.DTOs.Shared;

namespace VibLink.Models.DTOs.Response
{
    public record MessageDetailsDto : BaseResponseDto
    {
        public ObjectId Id { get; init; }
        public required UserDetailsDto Sender { get; init; }
        public string Content { get; init; } = string.Empty;
        public MessageTypeDto MessageType { get; init; }
        public ICollection<UserDetailsDto>? Recipients { get; init; }
        public ICollection<Dictionary<UserDetailsDto, DateTime>>? SeenBy { get; init; }
        public ICollection<Dictionary<UserDetailsDto, ReactionTypeDto>>? Reactions { get; init; }
    }
}
