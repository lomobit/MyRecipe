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

        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Добавление ингредиента.
        /// </summary>
        /// <param name="command">Команда на добавление ингредиента.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор добавленного ингредиента.</returns>
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
        /// <param name="query">Запрос на получение ингредиентов.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Ингредиенты.</returns>
        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(typeof(ApiResult<Pagination<IngredientDto>>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] IngredientGetQuery query, CancellationToken cancellationToken)
        {
            return await CallApiActionWithResultAsync(async () => await _mediator.Send(query, cancellationToken));
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<IngredientDto>>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] IngredientGetAllQuery query, CancellationToken cancellationToken)
        {
            return await CallApiActionWithResultAsync(async () => await _mediator.Send(query, cancellationToken));
        }

        /// <summary>
        /// Изменение ингредиента.
        /// </summary>
        /// <param name="command">Команда на изменение ингредиента.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Возвращет true - если изменение успешно произведено, false - если нет.</returns>
        [HttpPost]
        [Route("Edit")]
        [ProducesResponseType(typeof(ApiResult<bool>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit([FromBody] IngredientEditCommand command, CancellationToken cancellationToken)
        {
            return await CallApiActionWithResultAsync(async () => await _mediator.Send(command, cancellationToken));
        }
    }
}
