using AutoMapper;
using MyRecipe.Contracts.File;
using MyRecipeFiles.Infrastructure.Repositories.File;

namespace MyRecipeFiles.AppServices.File
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        
        public FileService(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<FileDto> DownloadAsync(Guid fileGuid, CancellationToken cancellationToken)
        {
            return _mapper.Map<FileDto>(await _fileRepository.DownloadAsync(fileGuid, cancellationToken));
        }

        /// <inheritdoc/>
        public async Task<Guid> UploadAsync(FileDto fileDto, CancellationToken cancellationToken)
        {
            return await _fileRepository.UploadAsync(fileDto, cancellationToken);
        }
    }
}
