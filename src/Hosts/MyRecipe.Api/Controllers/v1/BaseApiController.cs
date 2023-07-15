using Microsoft.AspNetCore.Mvc;
using MyRecipe.Contracts.Api;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MyRecipe.Api.Controllers.v1
{
    public abstract class BaseApiController : Controller
    {
        // TODO: вынести форматирование результата в отдельный midleware. Этот метод удалить. 
        protected virtual async Task<IActionResult> CallApiActionWithResultAsync<T>(Func<Task<T>> apiAction)
        {
            try
            {
                var result = await apiAction.Invoke();
                return Success(result);
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
        /// Успешное завершение вызова.
        /// </summary>
        /// <typeparam name="T">Тип возвращаемых данных.</typeparam>
        /// <param name="data">Данные.</param>
        /// <returns>Результат вызова API.</returns>
        protected IActionResult Success<T>(T? data)
        {
            return Ok(ApiResult<T>.SuccessResult(data));
        }

        /// <summary>
        /// Завершение вызова с ошибкой.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="errors">Ошибки.</param>
        /// <returns>Результат вызова API.</returns>
        protected IActionResult Error<T>(T? data, int statusCode, IDictionary errors)
        {
            // TODO: Добавить логирование ошибок.
            return StatusCode(statusCode, ApiResult<T>.ErrorResult(data, errors));
        }
    }
}
