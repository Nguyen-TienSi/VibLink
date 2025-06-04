using Microsoft.AspNetCore.Http;
using System.IO;
using VibLink.Models.Entities;

namespace VibLink.Helpers
{
    public class FileHandler
    {
        public static async Task<FileStorage> MapToFileStorageAsync(IFormFile formFile)
        {
            ArgumentNullException.ThrowIfNull(formFile);

            using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);

            return new FileStorage
            {
                FileName = formFile.FileName,
                ContentType = formFile.ContentType,
                Length = formFile.Length,
                FileData = memoryStream.ToArray(),
            };
        }
    }
}
