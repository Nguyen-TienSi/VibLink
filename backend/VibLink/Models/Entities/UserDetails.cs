using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using VibLink.Models.Enums;

namespace VibLink.Models.Entities
{
    [Collection("user_details")]
    public class UserDetails : BaseEntity
    {
        [BsonElement("firstName")]
        public string FirstName { get; set; } = string.Empty;
        [BsonElement("lastName")]
        public string LastName { get; set; } = string.Empty;
        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;
        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;
        [BsonElement("pictureUrl")]
        public string PictureUrl { get; set; } = string.Empty;
        [BsonElement("user_roles")]
        [BsonRepresentation(BsonType.String)]
        public ICollection<UserRole> UserRoles { get; set; } = [];
        [BsonElement("lastLogin")]
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;
        [BsonElement("friends")]
        public ICollection<UserDetails>? Friends { get; set; }
        [BsonElement("blockedUsers")]
        public ICollection<UserDetails>? BlockedUsers { get; set; }

    }
}
