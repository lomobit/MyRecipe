
namespace MyRecipe.Contracts.Enums.Api
{
    /// <summary>
    /// Тип сообщения.
    /// </summary>
    public enum ApiResultMessageTypeEnum
    {
        /// <summary>
        /// Не установлен.
        /// </summary>
        None = 0,

        /// <summary>
        /// Успешно.
        /// </summary>
        Success = 1,

        /// <summary>
        /// Предупреждение.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Ошибка.
        /// </summary>
        Error = 3
    }
}
