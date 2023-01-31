
using Microsoft.Extensions.Logging;
using MyRecipeLogging.Infrastructure.Repositories.Log;

namespace MyRecipe.Logging.Loggers
{
    public class DbLogger : ILogger, IDisposable
    {
        private readonly ILogRepository _logRepository;

        public DbLogger(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return this;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            _logRepository.AddLog(new MyRecipeLogging.Contracts.Log.LogDto
            {
                Message = formatter(state, exception),
                MessageType = MyRecipeLogging.Contracts.Enums.Log.LogMessageTypeEnum.Information,
                DateTime= DateTime.UtcNow,
            });
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
