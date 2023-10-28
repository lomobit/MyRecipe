using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipe.Contracts.Api;
using MyRecipe.Handlers.Contracts.Event;

namespace MyRecipe.Api.Controllers.v1;

/// <summary>
/// Контроллера для работы с событиями.
/// </summary>
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(ApiResult<ApiError>), statusCode: StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApiResult<ApiError>), statusCode: StatusCodes.Status500InternalServerError)]
public class EventController : BaseApiController
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Метода для добавления события.
    /// </summary>
    /// <param name="command">Команда добавления события.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(typeof(ApiResult<int>), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> Add([FromForm] EventAddCommand command, CancellationToken cancellationToken)
    {
        return await CallApiActionWithResultAsync(async () => await _mediator.Send(command, cancellationToken));
    }
}