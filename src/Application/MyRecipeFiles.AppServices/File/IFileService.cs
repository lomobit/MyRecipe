using Microsoft.AspNetCore.Http;
using MyRecipe.Contracts.File;

namespace MyRecipeFiles.AppServices.File
{
    public interface IFileService
    {
        /// <summary>
        /// Метод получения файла из базы данных.
        /// </summary>
        /// <param name="fileGuid">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель файла.</returns>
        Task<FileDto> DownloadAsync(Guid fileGuid, CancellationToken cancellationToken);

        /// <summary>
        /// Метод загрузки файла в базу данных.
        /// </summary>
        /// <param name="file">Даный файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Guid загруженного файла.</returns>
        Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken);
    }
}
