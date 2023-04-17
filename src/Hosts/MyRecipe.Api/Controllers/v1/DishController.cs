using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Dish;
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

        /// <summary>
        /// Метод получения списка блюд с краткой информацией.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список блюд с краткой информацией.</returns>
        [HttpGet]
        [Route("GetList")]
        [ProducesResponseType(typeof(ApiResult<List<DishForGridDto>>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Метод получения полной информации о блюде по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор блюда.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Полную информацию о блюде.</returns>
        [HttpGet]
        [Route("Get/{id}")]
        [ProducesResponseType(typeof(ApiResult<DishDto>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
