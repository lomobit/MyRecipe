using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipe.Contracts.Api;
using MyRecipe.Handlers.Contracts.Dish;

namespace MyRecipe.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResult<ApiError>), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ApiError>), statusCode: StatusCodes.Status500InternalServerError)]
    public class DishController : BaseApiController
    {
        private readonly IMediator _mediator;

        public DishController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Добавление блюда.
        /// </summary>
        /// <param name="command">Команда на добавление блюда.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор добавленного блюда.</returns>
        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(typeof(ApiResult<int>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] DishAddCommand command, CancellationToken cancellationToken)
        {
            return await CallApiActionWithResultAsync(async () => await _mediator.Send(command, cancellationToken));
        }
    }
}
