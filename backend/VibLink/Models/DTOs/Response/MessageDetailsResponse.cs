using MongoDB.Bson;
using VibLink.Models.DTOs.Shared;

namespace VibLink.Models.DTOs.Response
{
    public record MessageDetailsResponse : BaseResponse
    {
        public ObjectId Id { get; init; }
        public required UserDetailsResponse Sender { get; init; }
        public string Content { get; init; } = string.Empty;
        public MessageType MessageType { get; init; }
        public ICollection<UserDetailsResponse>? Recipients { get; init; }
        public ICollection<Dictionary<UserDetailsResponse, DateTime>>? SeenBy { get; init; }
        public ICollection<Dictionary<UserDetailsResponse, ReactionType>>? Reactions { get; init; }
    }
}
