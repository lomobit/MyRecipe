
namespace MyRecipe.Contracts.Api
{
    public class ApiError
    {
        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Message { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Описание ошибки.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Код ошибки.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Идентификатор запроса.
        /// </summary>
        public string? TraceId { get; set; }
    }
}
