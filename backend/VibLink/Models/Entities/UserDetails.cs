using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using VibLink.Models.Enumerations;

namespace VibLink.Models.Entities
{
    [Collection("userDetails")]
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
        [BsonElement("pictureId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? PictureId { get; set; } = null;
        [BsonIgnore]
        public FileStorage? Picture { get; set; } = null;
        [BsonElement("userRoles")]
        [BsonRepresentation(BsonType.String)]
        public ICollection<UserRole> UserRoles { get; set; } = [];
        [BsonElement("lastLogin")]
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;
        [BsonElement("friendIds")]
        public ICollection<ObjectId> FriendIds { get; set; } = [];
        [BsonIgnore]
        public ICollection<UserDetails>? Friends { get; set; }
        [BsonElement("blockedUserIds")]
        public ICollection<ObjectId> BlockedUserIds { get; set; } = [];
        [BsonIgnore]
        public ICollection<UserDetails>? BlockedUsers { get; set; }

    }
}
