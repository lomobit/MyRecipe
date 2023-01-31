
using MyRecipe.Contracts.Enums.Api;
using System.Collections;

namespace MyRecipe.Contracts.Api
{
    /// <summary>
    /// Результат выполнения методов Api.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// Данные
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Признак: выполнено успешно
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Коллекция с информацией
        /// </summary>
        public List<ApiResultMessage> Messages { get; set; }

        /// <summary>
        /// Конструктор <see cref="ApiResult{T}"/>
        /// </summary>
        public ApiResult()
        {
            Messages = new List<ApiResultMessage>();
        }

        /// <summary>
        /// Успешное выполнение операции.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Результат выполнения метода.</returns>
        public static ApiResult<T> SuccessResult(T? data)
        {
            return new ApiResult<T>()
            {
                Data = data,
                Success = true
            };
        }

        /// <summary>
        /// Успешное выполнение операции.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="messages">Сообщения.</param>
        /// <returns>Результат выполнения метода.</returns>
        public static ApiResult<T> ErrorResult(T? data, IDictionary? messages)
        {
            var apiResult = new ApiResult<T>()
            {
                Data = data,
                Success = false,
            };

            if (messages == null)
            {
                return apiResult;
            }

            foreach (var key in messages.Keys)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                apiResult.Messages.Add(new ApiResultMessage()
                {
                    Value = key?.ToString() ?? "Unknown",
                    Type = ApiResultMessageTypeEnum.Error,
                    Key = messages[key]?.ToString() ?? "Unknown",
                });
#pragma warning restore CS8604 // Possible null reference argument.
            }

            return apiResult;
        }
    }
}
