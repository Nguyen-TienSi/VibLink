using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using VibLink.Models.Enumerations;

namespace VibLink.Models.Entities
{
    [Collection("messages")]
    public class Message : BaseEntity
    {
        [BsonElement("senderId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId SenderId { get; set; }
        [BsonIgnore]
        public required UserDetails Sender { get; set; }
        [BsonElement("content")]
        public string Content { get; set; } = string.Empty;
        [BsonElement("messageType")]
        [BsonRepresentation(BsonType.String)]
        public MessageType MessageType { get; set; }
        [BsonElement("recipientIds")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ICollection<ObjectId> RecipientIds { get; set; } = [];
        [BsonIgnore]
        public ICollection<UserDetails>? Recipients { get; set; }
        [NotMapped]
        [BsonElement("seenBy")]
        public ICollection<Dictionary<UserDetails, DateTime>>? SeenBy { get; set; }
        [NotMapped]
        [BsonElement("reactions")]
        public ICollection<Dictionary<UserDetails, ReactionType>>? Reactions { get; set; }
    }
}
