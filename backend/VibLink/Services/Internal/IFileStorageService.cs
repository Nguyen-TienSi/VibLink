using MongoDB.Bson;
using VibLink.Models.Entities;

namespace VibLink.Services.Internal
{
    public interface IFileStorageService
    {
        Task<FileStorage?> GetPictureAsync(ObjectId id);
    }
}
