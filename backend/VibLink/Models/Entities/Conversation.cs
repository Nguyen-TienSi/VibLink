using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using VibLink.Models.Enumerations;

namespace VibLink.Models.Entities
{
    [Collection("conversations")]
    public class Conversation : BaseEntity
    {
        [BsonElement("chatName")]
        public string ChatName { get; set; } = string.Empty;
        [BsonElement("chatPictureId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? ChatPictureId { get; set; } = null;
        [BsonIgnore]
        public FileStorage? ChatPicture { get; set; } = null;
        [BsonElement("conversationType")]
        [BsonRepresentation(BsonType.String)]
        public ConversationType ConversationType { get; set; } = ConversationType.PERSONAL;
        [BsonElement("participantIds")]
        public ICollection<ObjectId> ParticipantIds { get; set; } = []; 
        [BsonIgnore]
        public ICollection<UserDetails>? Participants { get; set; }
        [BsonElement("messageIds")]
        public ICollection<ObjectId> MessageIds { get; set; } = [];
        [BsonIgnore]
        public ICollection<Message>? Messages { get; set; }
    }
}
