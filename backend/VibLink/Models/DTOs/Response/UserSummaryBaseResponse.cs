using MongoDB.Bson;

namespace VibLink.Models.DTOs.Response
{
    public record UserSummaryBaseResponse
    {
        public string Id { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string PictureUrl { get; init; } = string.Empty;
    }
}
