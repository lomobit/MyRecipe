using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyRecipe.Contracts.File;

namespace MyRecipeFiles.Infrastructure.Repositories.File
{
    public class FileRepository : IFileRepository
    {
        private readonly MyRecipeFilesDbContext _context;

        public FileRepository(MyRecipeFilesDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Domain.File> DownloadAsync(Guid fileGuid, CancellationToken cancellationToken)
        {
            var result = await _context.Files
                .AsNoTracking()
                .Where(f => f.Guid == fileGuid)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (result == null)
            {
                var ex = new ValidationException($"Файл с идентификатором \"{fileGuid}\" не существует");
                ex.Data.Add("Файл", $"Файл с идентификатором \"{fileGuid}\" не существует");

                throw ex;
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<Guid> UploadAsync(FileDto fileDto, CancellationToken cancellationToken)
        {
            var newGuid = Guid.NewGuid();
            try
            {
                await _context.Files.AddAsync(new Domain.File
                {
                    Guid = newGuid,
                    Name = fileDto.Name,
                    Content = fileDto.Content,
                    Size = fileDto.Size,
                }, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            finally
            {
                _context.ChangeTracker.Clear();
            }

            return newGuid;
        }
    }
}
