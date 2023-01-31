using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyRecipe.Infrastructure;

namespace MyRecipe.Migrator
{
    public class MigrationsWorker : BackgroundService
    {
        private readonly int _retryCount = 3;
        private readonly MyRecipeDbContext _context;
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public MigrationsWorker(MyRecipeDbContext context, ILogger<MigrationsWorker> logger, IHostApplicationLifetime applicationLifetime)
        {
            _context = context;
            _logger = logger;
            _applicationLifetime = applicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting MyRecipe migrations...");
            var attempt = 0;
            do
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                attempt++;
                try
                {
                    await _context.Database.MigrateAsync(cancellationToken);

                    _logger.LogInformation("MyRecipe migrations ended.");
                    _applicationLifetime.StopApplication();
                    return;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Try {attempt} connection to Database server FAILED");
                }

                var nextDelayTime = TimeSpan.FromMilliseconds(attempt * 1000);

                _logger.LogInformation($"Will try in {nextDelayTime} milliseconds");
                await Task.Delay(nextDelayTime, cancellationToken);
            }
            while (_retryCount > attempt);

            if (attempt == _retryCount)
                Environment.ExitCode = 1;

            _applicationLifetime.StopApplication();
        }
    }
}
