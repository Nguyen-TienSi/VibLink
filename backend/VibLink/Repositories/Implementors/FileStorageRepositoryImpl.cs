using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Threading.Tasks;
using VibLink.Data;
using VibLink.Models.Entities;

namespace VibLink.Repositories.Implementors
{
    public class FileStorageRepositoryImpl : MongoRepositoryImpl<FileStorage>, IFileStorageRepository
    {
        private readonly GridFSBucket _gridFSBucket;

        public FileStorageRepositoryImpl(VibLinkDbContext dbContext, GridFSBucket gridFSBucket) : base(dbContext)
        {
            _gridFSBucket = gridFSBucket;
        }

        public async Task<ObjectId> UploadFileAsync(IFormFile formFile)
        {
            ArgumentNullException.ThrowIfNull(formFile);

            using var stream = formFile.OpenReadStream();
            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument
                {
                    { "contentType", formFile.ContentType }
                }
            };
            var id = await _gridFSBucket.UploadFromStreamAsync(formFile.FileName, stream, options);
            return (ObjectId)id;
        }

        public async Task<(Stream? FileStream, string? FileName, string? ContentType)> DownloadFileAsync(ObjectId fileId)
        {
            var fileInfo = await _gridFSBucket.Find(Builders<GridFSFileInfo>.Filter.Eq("_id", fileId)).FirstOrDefaultAsync();
            if (fileInfo == null)
                return (null, null, null);

            var memoryStream = new MemoryStream();
            await _gridFSBucket.DownloadToStreamAsync(fileId, memoryStream);
            memoryStream.Position = 0;

            var fileName = fileInfo.Filename;
            var contentType = fileInfo.Metadata?.GetValue("contentType", null)?.AsString;

            return (memoryStream, fileName, contentType);
        }
    }
}
