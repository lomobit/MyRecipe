using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyRecipe.Contracts.File;
using MyRecipeFiles.Infrastructure.Repositories.File;

namespace MyRecipeFiles.AppServices.File
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        
        public FileService(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<FileDto> DownloadAsync(Guid fileGuid, CancellationToken cancellationToken)
        {
            return _mapper.Map<FileDto>(await _fileRepository.DownloadAsync(fileGuid, cancellationToken));
        }

        /// <inheritdoc/>
        public async Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken)
        {
            // Валидация загружаемого файла.
            const int _1GB = 1073741824;
            if (file.Length > _1GB)
            {
                var ex = new ValidationException($"Попытка загрузить файл слишком большого размера. Максимальный размер файла: 1ГБ");
                ex.Data.Add("Файл", $"Попытка загрузить файл слишком большого размера. Максимальный размер файла: 1ГБ");

                throw ex;
            }

            if (file.Length == 0)
            {
                var ex = new ValidationException($"Попытка загрузить пустой файл.");
                ex.Data.Add("Файл", $"Попытка загрузить пустой файл.");

                throw ex;
            }
            
            // Подготовка загружаемого файла к закгрузке в БД.
            byte[] fileContent;
            using (var stream = new MemoryStream((int)file.Length))
            {
                await file.CopyToAsync(stream, cancellationToken);
                fileContent = stream.ToArray();
            }

            var fileDto = new FileDto()
            {
                Name = file.Name,
                Content = fileContent,
                Size = file.Length
            };
            
            // Загрузка файла.
            return await _fileRepository.UploadAsync(fileDto, cancellationToken);
        }
    }
}
