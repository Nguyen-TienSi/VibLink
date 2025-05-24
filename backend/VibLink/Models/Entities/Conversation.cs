using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using VibLink.Models.Enums;

namespace VibLink.Models.Entities
{
    [Collection("conversations")]
    public class Conversation : BaseEntity
    {
        [BsonElement("chatName")]
        public string ChatName { get; set; } = string.Empty;
        [BsonElement("chatPictureUrl")]
        public string ChatPictureUrl { get; set; } = string.Empty;
        [BsonElement("conversationType")]
        [BsonRepresentation(BsonType.String)]
        public ConversationType ConversationType { get; set; } = ConversationType.PERSONAL;
        [BsonElement("participants")]
        public ICollection<UserDetails>? Participants { get; set; }
        [BsonElement("lastMessage")]
        public ICollection<Message>? Messages { get; set; }
    }
}
