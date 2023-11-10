using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Event;
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
    /// Метод для добавления события.
    /// </summary>
    /// <param name="command">Команда добавления события.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(typeof(ApiResult<int>), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> Add([FromBody] EventAddCommand command, CancellationToken cancellationToken)
    {
        return await CallApiActionWithResultAsync(async () => await _mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetPage")]
    [ProducesResponseType(typeof(ApiResult<Pagination<EventForGridDto>>), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPage(CancellationToken cancellation)
    {
        return Ok();
    }
}