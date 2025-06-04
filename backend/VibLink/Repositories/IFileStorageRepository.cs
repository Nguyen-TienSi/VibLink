using MongoDB.Bson;
using VibLink.Models.Entities;

namespace VibLink.Repositories
{
    public interface IFileStorageRepository : IMongoRepository<FileStorage>
    {
        Task<ObjectId> UploadFileAsync(IFormFile formFile);
        Task<(Stream? FileStream, string? FileName, string? ContentType)> DownloadFileAsync(ObjectId fileId);
    }
}
