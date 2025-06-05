using AutoMapper;
using MongoDB.Bson;
using VibLink.Models.Entities;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class FileStorageServiceImpl : IFileStorageService
    {
        private readonly IFileStorageRepository _fileStorageRepository;
        private readonly IMapper _mapper;

        public FileStorageServiceImpl(IFileStorageRepository fileStorageRepository, IMapper mapper)
        {
            _fileStorageRepository = fileStorageRepository;
            _mapper = mapper;
        }

        public async Task<FileStorage?> GetPictureAsync(ObjectId id)
        {
            return await _fileStorageRepository.FindByIdAsync(id);
        }
    }
}
