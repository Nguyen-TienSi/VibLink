using MongoDB.Bson.Serialization.Attributes;
using VibLink.Domain.Enums;

namespace VibLink.Domain.Entities
{
    public class Conversation : BaseEntity
    {
        [BsonElement("chatName")]
        public string ChatName { get; set; } = string.Empty;
        [BsonElement("chatPictureUrl")]
        public string ChatPictureUrl { get; set; } = string.Empty;
        [BsonElement("conversationType")]
        public ConversationType Type { get; set; } = ConversationType.PERSONAL;
        [BsonElement("isMuted")]
        public ICollection<UserDetails>? IsMuted { get; set; }
        [BsonElement("participants")]
        public ICollection<UserDetails>? Participants { get; set; }
        [BsonElement("lastMessage")]
        public ICollection<Message>? Messages { get; set; }
    }
}
