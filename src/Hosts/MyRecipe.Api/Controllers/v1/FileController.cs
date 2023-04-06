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
        [Produces("application/octet-stream", "text/plain")]
        [ProducesResponseType(typeof(FileContentResult), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Download([FromRoute] Guid guid, CancellationToken cancellationToken)
        {
            Response.ContentType = "text/plain";

            try
            {
                var fileDto = await _mediator.Send(new FileDownloadRequest(guid), cancellationToken);
                return new FileContentResult(fileDto.Content, "application/octet-stream")
                {
                    FileDownloadName = fileDto.Name
                };
            }
            catch (ValidationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                // TODO: добавить логирование
                return StatusCode(StatusCodes.Status500InternalServerError, "Сервис временно недоступен");
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
            var command = new FileUploadCommand(file);
            return await CallApiActionWithResultAsync(async () => await _mediator.Send(command, cancellationToken));
        }
    }
}
