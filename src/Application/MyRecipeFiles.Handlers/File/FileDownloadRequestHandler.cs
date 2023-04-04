using MediatR;
using MyRecipe.Contracts.File;
using MyRecipeFiles.AppServices.File;
using MyRecipeFiles.Handlers.Contracts.File;

namespace MyRecipeFiles.Handlers.File;

public class FileDownloadRequestHandler : IRequestHandler<FileDownloadRequest, FileDto>
{
    private readonly IFileService _fileService;

    public FileDownloadRequestHandler(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    public async Task<FileDto> Handle(FileDownloadRequest request, CancellationToken cancellationToken)
    {
        return await _fileService.DownloadAsync(request.FileGuid, cancellationToken);
    }
}