using MyRecipe.Contracts.File;
using MyRecipeFiles.Infrastructure.Repositories.File;

namespace MyRecipeFiles.AppServices.File
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<Guid> UploadAsync(FileDto fileDto, CancellationToken cancellationToken)
        {
            return await _fileRepository.UploadAsync(fileDto, cancellationToken);
        }
    }
}
