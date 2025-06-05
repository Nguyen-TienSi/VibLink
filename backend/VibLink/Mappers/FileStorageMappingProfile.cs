using AutoMapper;
using VibLink.Models.Entities;
using VibLink.Mappers.Converters;

namespace VibLink.Mappers
{
    public class FileStorageMappingProfile : Profile
    {
        public FileStorageMappingProfile()
        {
            CreateMap<IFormFile, FileStorage>()
                .ConvertUsing<IFormFileToFileStorageConverter>();
        }
    }
}
