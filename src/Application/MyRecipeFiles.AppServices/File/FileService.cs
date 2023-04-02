using MyRecipeFiles.Handlers.Contracts.File;

namespace MyRecipeFiles.AppServices.File
{
    public class FileService : IFileService
    {
        public async Task<Guid> UploadAsync(FileUploadCommand command, CancellationToken cancellationToken)
        {

        }
    }
}
