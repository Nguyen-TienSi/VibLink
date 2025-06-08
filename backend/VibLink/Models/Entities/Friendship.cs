using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using VibLink.Models.Enumerations;

namespace VibLink.Models.Entities
{
    [Collection("friendships")]
    public class Friendship : BaseEntity
    {
        [BsonElement("requesterId")]
        public ObjectId RequesterId { get; set; }
        [BsonIgnore]
        public UserDetails Requester { get; set; } = null!;
        [BsonElement("addresseeId")]
        public ObjectId AddresseeId { get; set; }
        [BsonIgnore]
        public UserDetails Addressee { get; set; } = null!;
        [BsonElement("friendRequestStatus")]
        [BsonRepresentation(BsonType.String)]
        public FriendshipRequestStatus FriendshipRequestStatus { get; set; } = FriendshipRequestStatus.PENDING;
    }
}
