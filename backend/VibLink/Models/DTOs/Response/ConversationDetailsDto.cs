using MongoDB.Bson;
using VibLink.Models.DTOs.Shared;

namespace VibLink.Models.DTOs.Response
{
    public record ConversationDetailsDto : BaseResponseDto
    {
        public ObjectId Id { get; init; }
        public string ChatName { get; init; } = string.Empty;
        public string ChatPictureUrl { get; init; } = string.Empty;
        public ConversationTypeDto ConversationTypeDto { get; init; }
        public ICollection<UserDetailsDto>? Participants { get; init; }
        public ICollection<MessageDetailsDto>? Messages { get; init; }
    }
}
