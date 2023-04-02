using MyRecipeFiles.Handlers.Contracts.File;

namespace MyRecipeFiles.AppServices.File
{
    public interface IFileService
    {
        /// <summary>
        /// Метод загрузки файла в базу данных.
        /// </summary>
        /// <param name="command">Данные файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Guid загруженного файла.</returns>
        Task<Guid> UploadAsync(FileUploadCommand command, CancellationToken cancellationToken);
    }
}
