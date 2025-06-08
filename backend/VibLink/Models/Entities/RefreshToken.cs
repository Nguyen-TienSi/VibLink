using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace VibLink.Models.Entities
{
    [Collection("refreshTokens")]
    public class RefreshToken : BaseEntity
    {
        [BsonElement("token")]
        public string Token { get; set; } = string.Empty;
        [BsonElement("expires")]
        public DateTime Expires { get; set; }
        [BsonElement("createdByIp")]
        public string? CreatedByIp { get; set; }
        [BsonElement("revoked")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? Revoked { get; set; }
        [BsonElement("revokedByIp")]
        public string? RevokedByIp { get; set; }
        [BsonIgnore]
        public bool IsExpired => DateTime.UtcNow >= Expires;
        [BsonIgnore]
        public bool IsRevoked => Revoked != null;
        [BsonIgnore]
        public bool IsActive => !IsRevoked && !IsExpired;
        [BsonElement("userDetailsId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId UserDetailsId { get; set; }
    }
}
