using MyRecipe.Contracts.File;

namespace MyRecipeFiles.Infrastructure.Repositories.File
{
    public interface IFileRepository
    {
        /// <summary>
        /// Метод загрузки файла в базу данных.
        /// </summary>
        /// <param name="fileDto">Данные файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Guid загруженного файла.</returns>
        Task<Guid> UploadAsync(FileDto fileDto, CancellationToken cancellationToken);
    }
}
