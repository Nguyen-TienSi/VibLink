using MongoDB.Bson;

namespace VibLink.Models.DTOs.Response
{
    public record UserSummaryBaseDto
    {
        public ObjectId Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string PictureUrl { get; init; } = string.Empty;
    }
}
