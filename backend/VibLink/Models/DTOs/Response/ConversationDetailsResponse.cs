using MongoDB.Bson;
using VibLink.Models.DTOs.Shared;

namespace VibLink.Models.DTOs.Response
{
    public record ConversationDetailsResponse : BaseResponse
    {
        public string Id { get; init; } = string.Empty;
        public string ChatName { get; init; } = string.Empty;
        public string ChatPictureUrl { get; init; } = string.Empty;
        public ConversationType ConversationType { get; init; }
        public ICollection<UserDetailsResponse>? Participants { get; init; }
        public ICollection<MessageDetailsResponse>? Messages { get; init; }
    }
}
