using MyRecipe.Contracts.File;

namespace MyRecipeFiles.Infrastructure.Repositories.File
{
    public interface IFileRepository
    {
        /// <summary>
        /// Метод получения файла из базы данных.
        /// </summary>
        /// <param name="fileGuid">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель файла.</returns>
        Task<Domain.File> DownloadAsync(Guid fileGuid, CancellationToken cancellationToken); 
        
        /// <summary>
        /// Метод загрузки файла в базу данных.
        /// </summary>
        /// <param name="fileDto">Данные файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Guid загруженного файла.</returns>
        Task<Guid> UploadAsync(FileDto fileDto, CancellationToken cancellationToken);
    }
}
