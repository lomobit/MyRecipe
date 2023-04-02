using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Ingredient;
using MyRecipe.Handlers.Contracts.Ingredient;

namespace MyRecipe.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResult<ApiError>), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult<ApiError>), statusCode: StatusCodes.Status500InternalServerError)]
    public class IngredientController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public IngredientController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Добавление ингредиента.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(typeof(ApiResult<int>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] IngredientAddCommand command, CancellationToken cancellationToken)
        {
            return await CallApiActionWithResultAsync(async () => await _mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// Получение ингредиентов.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(typeof(ApiResult<Pagination<IngredientDto>>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] IngredientGetQuery query, CancellationToken cancellationToken)
        {
            return await CallApiActionWithResultAsync(async () => await _mediator.Send(query, cancellationToken));
        }

        /// <summary>
        /// Изменение ингредиента.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Edit")]
        [ProducesResponseType(typeof(ApiResult<bool>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit([FromBody] IngredientEditCommand command, CancellationToken cancellationToken)
        {
            return await CallApiActionWithResultAsync(async () => await _mediator.Send(command, cancellationToken));
        }
    }
}
