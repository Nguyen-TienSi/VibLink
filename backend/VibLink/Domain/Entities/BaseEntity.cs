using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VibLink.Domain.Entities
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [BsonElement("deletedAt")]
        public DateTime? DeletedAt { get; set; } = null;

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; } = false;
        [BsonElement("version")]
        public int Version { get; set; } = 0;
    }
}
