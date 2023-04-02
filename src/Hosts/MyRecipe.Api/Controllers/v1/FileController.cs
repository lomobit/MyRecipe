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

        [HttpPost]
        [Route("Upload")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApiResult<Guid>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
        {
            if (file.Length > int.MaxValue)
            {
                var errors = new Dictionary<string, string>()
                {
                    { "Загрузка файла", "Попытка загрузить файл слишком большого размера. Максимальный размер файла: 2ГБ" }
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
                Name = file.Name,
                Content = fileContent,
                Size = file.Length
            };
            
            return await CallApiActionWithResultAsync(async () => await _mediator.Send(command, cancellationToken));
        }
    }
}
