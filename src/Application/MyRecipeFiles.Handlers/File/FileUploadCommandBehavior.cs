using MediatR;
using MyRecipeFiles.Handlers.Contracts.File;
using System.ComponentModel.DataAnnotations;

namespace MyRecipeFiles.Handlers.File
{
    public class FileUploadCommandBehavior : IPipelineBehavior<FileUploadCommand, Guid>
    {
        public async Task<Guid> Handle(FileUploadCommand command, RequestHandlerDelegate<Guid> next, CancellationToken cancellationToken)
        {
            // Валидация загружаемого файла
            const int _1GB = 1073741824;
            if (command.File.Length > _1GB)
            {
                var ex = new ValidationException($"Попытка загрузить файл слишком большого размера. Максимальный размер файла: 1ГБ");
                ex.Data.Add("Файл", $"Попытка загрузить файл слишком большого размера. Максимальный размер файла: 1ГБ");

                throw ex;
            }

            if (command.File.Length == 0)
            {
                var ex = new ValidationException($"Попытка загрузить пустой файл.");
                ex.Data.Add("Файл", $"Попытка загрузить пустой файл.");

                throw ex;
            }

            return await next();
        }
    }
}
