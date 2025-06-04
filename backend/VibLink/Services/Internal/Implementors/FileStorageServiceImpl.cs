using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class FileStorageServiceImpl : IFileStorageService
    {
        private readonly IFileStorageRepository _fileMetadataRepository;

        public FileStorageServiceImpl(IFileStorageRepository fileMetadataRepository)
        {
            _fileMetadataRepository = fileMetadataRepository;
        }
    }
}
