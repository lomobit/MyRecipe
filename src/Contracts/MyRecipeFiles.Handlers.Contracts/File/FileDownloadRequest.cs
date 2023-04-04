using MediatR;
using MyRecipe.Contracts.File;

namespace MyRecipeFiles.Handlers.Contracts.File;

public class FileDownloadRequest : IRequest<FileDto>
{
    /// <summary>
    /// Идентификатор файла.
    /// </summary>
    public Guid FileGuid { get; set; }

    public FileDownloadRequest(Guid guid)
    {
        FileGuid = guid;
    }
}