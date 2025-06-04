using MongoDB.Bson;
using VibLink.Models.DTOs.Shared;

namespace VibLink.Models.DTOs.Response
{
    public record UserDetailsResponse : BaseResponse
    {
        public ObjectId Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string PictureUrl { get; init; } = string.Empty;
        public IEnumerable<UserRole> UserRoles { get; init; } = [];
        public DateTime LastLogin { get; init; }
    }
}
