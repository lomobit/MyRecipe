using MediatR;
using Microsoft.AspNetCore.Http;

namespace MyRecipeFiles.Handlers.Contracts.File
{
    public class FileUploadCommand : IRequest<Guid>
    {
        /// <summary>
        /// Файл.
        /// </summary>
        public IFormFile File { get; set; }

        public FileUploadCommand(IFormFile file)
        {
            File = file;
        }
    }
}
