using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace VibLink.Models.Entities
{
    [Collection("file_metadata")]
    public class FileStorage : BaseEntity
    {
        [BsonElement("fileName")]
        public string FileName { get; set; } = string.Empty;
        [BsonElement("contentType")]
        public string ContentType { get; set; } = string.Empty;
        [BsonElement("length")]
        public long Length { get; set; }
        [BsonElement("fileData")]
        [BsonRepresentation(BsonType.Binary)]
        public byte[] FileData { get; set; } = [];
    }
}
