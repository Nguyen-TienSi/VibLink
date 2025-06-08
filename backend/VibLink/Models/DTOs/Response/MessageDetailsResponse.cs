using MongoDB.Bson;
using VibLink.Models.DTOs.Shared;

namespace VibLink.Models.DTOs.Response
{
    public record MessageDetailsResponse : BaseResponse
    {
        public string Id { get; init; } = string.Empty;
        public required UserSummaryBaseResponse Sender { get; init; }
        public string Content { get; init; } = string.Empty;
        public MessageType MessageType { get; init; }
        public ICollection<UserSummaryBaseResponse>? Recipients { get; init; }
        public ICollection<Dictionary<UserSummaryBaseResponse, DateTime>>? SeenBy { get; init; }
        public ICollection<Dictionary<UserSummaryBaseResponse, ReactionType>>? Reactions { get; init; }
    }
}
