using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipe.Contracts.Api;
using MyRecipeFiles.Handlers.Contracts.File;

namespace MyRecipe.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ProducesResponseType(typeof(ApiResult<ApiError>), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ApiError>), statusCode: StatusCodes.Status500InternalServerError)]
    public class FileController : BaseApiController
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{guid}")]
        [Produces("application/octet-stream")]
        [ProducesResponseType(typeof(FileContentResult), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Download([FromRoute] Guid guid, CancellationToken cancellationToken)
        {
            try
            {
                var fileDto = await _mediator.Send(new FileDownloadRequest(guid), cancellationToken);

                Response.Headers.Add("Content-Disposition", "inline");

                return new FileContentResult(fileDto.Content, "application/octet-stream")
                {
                    FileDownloadName = fileDto.Name
                };
            }
            catch (ValidationException ex)
            {
                return Error(
                    default(string),
                    StatusCodes.Status400BadRequest,
                    ex.Data);
            }
            catch (Exception ex)
            {
                return Error(
                    default(string),
                    StatusCodes.Status500InternalServerError,
                    ex.Data);
            }
        }

        
        /// <summary>
        /// Загружает файл в БД.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Guid загруженного файла.</returns>
        [HttpPost]
        [Route("Upload")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApiResult<Guid>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
        {
            const int _1GB = 1073741824;
            if (file.Length > _1GB)
            {
                var errors = new Dictionary<string, string>()
                {
                    { "Загрузка файла", "Попытка загрузить файл слишком большого размера. Максимальный размер файла: 1ГБ" }
                };

                return Error((Guid?)null, StatusCodes.Status400BadRequest, errors);
            }
            
            if (file.Length == 0)
            {
                var errors = new Dictionary<string, string>()
                {
                    { "Загрузка файла", "Попытка загрузить пустой файл." }
                };

                return Error((Guid?)null, StatusCodes.Status400BadRequest, errors);
            }

            byte[] fileContent;
            using (var stream = new MemoryStream((int)file.Length))
            {
                await file.CopyToAsync(stream, cancellationToken);
                fileContent = stream.ToArray();
            }

            var command = new FileUploadCommand
            {
                Name = file.FileName,
                Content = fileContent,
                Size = file.Length
            };
            
            return await CallApiActionWithResultAsync(async () => await _mediator.Send(command, cancellationToken));
        }
    }
}
