using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using VibLink.Models.Enums;

namespace VibLink.Models.Entities
{
    [Collection("messages")]
    public class Message : BaseEntity
    {
        [BsonElement("sender")]
        public required UserDetails Sender { get; set; }
        [BsonElement("content")]
        public string Content { get; set; } = string.Empty;
        [BsonElement("messageType")]
        [BsonRepresentation(BsonType.String)]
        public MessageType MessageType { get; set; }
        [BsonElement("recipients")]

        public ICollection<UserDetails>? Recipients { get; set; }
        [NotMapped]
        [BsonElement("seenBy")]
        public ICollection<Dictionary<UserDetails, DateTime>>? SeenBy { get; set; }
        [NotMapped]
        [BsonElement("reactions")]
        public ICollection<Dictionary<UserDetails, ReactionType>>? Reactions { get; set; }
    }
}
