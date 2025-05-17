using MongoDB.Bson.Serialization.Attributes;

namespace VibLink.Domain.Entities
{
    public class Friendship : BaseEntity
    {
        [BsonElement("requester")]
        public required UserDetails Requester { get; set; }
        [BsonElement("addressee")]

        public required UserDetails Addressee { get; set; }
        [BsonElement("isAccepted")]
        public bool IsAccepted { get; set; } = false;
    }
}
