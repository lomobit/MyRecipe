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
