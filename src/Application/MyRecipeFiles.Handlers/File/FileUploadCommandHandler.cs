using MediatR;
using MyRecipe.Contracts.File;
using MyRecipeFiles.AppServices.File;
using MyRecipeFiles.Handlers.Contracts.File;

namespace MyRecipeFiles.Handlers.File
{
    public class FileUploadCommandHandler : IRequestHandler<FileUploadCommand, Guid>
    {
        private readonly IFileService _fileService;

        public FileUploadCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<Guid> Handle(FileUploadCommand command, CancellationToken cancellationToken)
        {
            var fileDto = new FileDto()
            {
                Name = command.Name,
                Content = command.Content,
                Size = command.Size
            };

            return await _fileService.UploadAsync(fileDto, cancellationToken);
        }
    }
}
