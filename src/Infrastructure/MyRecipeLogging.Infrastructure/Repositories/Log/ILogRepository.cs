using MyRecipeLogging.Contracts.Log;

namespace MyRecipeLogging.Infrastructure.Repositories.Log
{
    public interface ILogRepository
    {
        /// <summary>
        /// Добавление записи лога в базу данных.
        /// </summary>
        /// <param name="logDto">Лог.</param>
        void AddLog(LogDto logDto);
    }
}
