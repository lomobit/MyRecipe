using MyRecipeLogging.Contracts.Enums.Log;

namespace MyRecipeLogging.Domain
{
    public class Log
    {
        /// <summary>
        /// Идентификатор лога.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Время записи лога.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Сообщение лога.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Message { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Тип сообщения.
        /// </summary>
        public LogMessageTypeEnum MessageType { get; set; }

        /// <summary>
        /// Трассировка стека.
        /// </summary>
        public string? StackTrace { get; set; }
    }
}