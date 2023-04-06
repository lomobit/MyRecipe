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
            byte[] fileContent;
            using (var stream = new MemoryStream((int)command.File.Length))
            {
                await command.File.CopyToAsync(stream, cancellationToken);
                fileContent = stream.ToArray();
            }

            var fileDto = new FileDto()
            {
                Name = command.File.Name,
                Content = fileContent,
                Size = command.File.Length
            };

            return await _fileService.UploadAsync(fileDto, cancellationToken);
        }
    }
}
