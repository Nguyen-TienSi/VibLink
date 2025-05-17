using MongoDB.Bson.Serialization.Attributes;

namespace VibLink.Domain.Entities
{
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
        [BsonElement("isAdmin")]
        public bool IsAdmin { get; set; } = false;
        [BsonElement("lastLogin")]
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;
        [BsonElement("friends")]
        public ICollection<UserDetails>? Friends { get; set; }
        [BsonElement("blockedUsers")]
        public ICollection<UserDetails>? BlockedUsers { get; set; }

    }
}
