using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using VibLink.Domain.Enums;

namespace VibLink.Domain.Entities
{
    public class Message : BaseEntity
    {
        [BsonElement("sender")]
        public required UserDetails Sender { get; set; }
        [BsonElement("content")]
        public string Content { get; set; } = string.Empty;
        [BsonElement("mediaType")]
        public MessageType MediaType { get; set; } = MediaType.Unknown;
        [BsonElement("mediaUrl")]
        public string MediaUrl { get; set; } = string.Empty;
        [BsonElement("recipients")]

        public ICollection<UserDetails>? Recipients { get; set; }
        [BsonElement("seenBy")]
        public ICollection<Dictionary<UserDetails, DateTime>>? SeenBy { get; set; }
        [BsonElement("reactions")]
        public ICollection<Dictionary<UserDetails, ReactionType>>? Reactions { get; set; }
    }
}
