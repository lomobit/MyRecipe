using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Okei;
using MyRecipe.Handlers.Contracts.Okei;

namespace MyRecipe.Api.Controllers.v1;

[Route("api/v1/[controller]")]
[ProducesResponseType(typeof(ApiResult<ApiError>), statusCode: StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApiResult<ApiError>), statusCode: StatusCodes.Status500InternalServerError)]
public class OkeiController : BaseApiController
{
    private readonly IMediator _mediator;

    public OkeiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<OkeiDto>>), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return await CallApiActionWithResultAsync(async () => await _mediator.Send(new OkeiGetAllQuery(), cancellationToken));
    }
}