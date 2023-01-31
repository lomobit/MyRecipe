using Castle.Components.DictionaryAdapter.Xml;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyRecipe.Infrastructure;
using MyRecipe.Migrator;


await CreateHostBuilder(args)
    .RunConsoleAsync(options => options.SuppressStatusMessages = true);

static IHostBuilder CreateHostBuilder(string[] args) =>
    new HostBuilder()
        .ConfigureAppConfiguration((hostContext, configApp) =>
        {
            configApp.SetBasePath(Directory.GetCurrentDirectory());
            configApp.AddJsonFile("appsettings.json");
            //configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json");
            configApp.AddCommandLine(args);
            configApp.AddEnvironmentVariables();
        })
        .ConfigureServices((hostContext, services) =>
        {
            const string connectionStringName = "MyRecipeDb";

            var connectionString = hostContext.Configuration.GetConnectionString(connectionStringName);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    $"Не найдена строка подключения с именем {connectionStringName}");
            }

            services.AddDbContext<MyRecipeDbContext>(options => options.UseNpgsql(connectionString));
            services.AddHostedService<MigrationsWorker>();
        });
