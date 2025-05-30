using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using VibLink.Models.Enumurations;

namespace VibLink.Models.Entities
{
    [Collection("friendships")]
    public class Friendship : BaseEntity
    {
        [BsonElement("requester")]
        public required UserDetails Requester { get; set; }
        [BsonElement("addressee")]

        public required UserDetails Addressee { get; set; }
        [BsonElement("friend_request_status")]
        public FriendRequestStatus FriendRequestStatus { get; set; }
    }
}
