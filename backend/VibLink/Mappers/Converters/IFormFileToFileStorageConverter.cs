using AutoMapper;
using VibLink.Models.Entities;

namespace VibLink.Mappers.Converters
{
    public class IFormFileToFileStorageConverter : ITypeConverter<IFormFile, FileStorage>
    {
        FileStorage ITypeConverter<IFormFile, FileStorage>.Convert(IFormFile source, FileStorage destination, ResolutionContext context)
        {
            ArgumentNullException.ThrowIfNull(source);

            using var memoryStream = new MemoryStream();
            source.CopyTo(memoryStream);

            return new FileStorage
            {
                FileName = source.FileName,
                ContentType = source.ContentType,
                Length = source.Length,
                FileData = memoryStream.ToArray(),
            };
        }
    }
}
