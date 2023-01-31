using MyRecipe.Contracts.Enums.Api;

namespace MyRecipe.Contracts.Api
{
    /// <summary>
    /// Сообщение для SaveResult
    /// </summary>
    public class ApiResultMessage
    {
        /// <summary>
        /// Ключ
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Key { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Тип
        /// </summary>
        public ApiResultMessageTypeEnum Type { get; set; }

        /// <summary>
        /// Значение сообщения
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Value { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
