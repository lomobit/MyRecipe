using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyRecipe.Common.ComponentRegistrar;
using MyRecipe.Logging.Loggers;
using MyRecipeLogging.Infrastructure;
using MyRecipeLogging.Infrastructure.MappingProfiles;
using MyRecipeLogging.Infrastructure.Repositories.Log;

namespace MyRecipeLogging.ComponentRegistrar
{
    public static class MyRecipeLoggingRegistrar
    {
        /// <summary>
        /// Добавление зависимостей для MyRecipeLogging.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeLogging(this IServiceCollection services)
        {
            // Добавление DbContext'ов
            services.AddMyRecipeLoggingDbContexts();

            // Добавление репозиториев для работы с базой данных.
            services.AddMyRecipeLoggingRepositories();

            // Добавление логгеров
            services.AddMyRecipeLoggers();

            // Добавление автомапперов.
            services.AddMyRecipeLoggingMappers();

            return services;
        }

        /// <summary>
        /// Добавление DbContext'ов.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeLoggingDbContexts(this IServiceCollection services)
        {
            services.AddDbContext<MyRecipeLoggingDbContext>(AddMyRecipeLoggingDbContext, ServiceLifetime.Scoped);

            return services;
        }

        /// <summary>
        /// Добавление репозиториев для работы с базой данных.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeLoggingRepositories(this IServiceCollection services)
        {
            // Репозитории MyRecipeLoggingDbContext'а
            services.AddScoped<ILogRepository, LogRepository>();

            return services;
        }

        /// <summary>
        /// Добавление логгеров.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeLoggers(this IServiceCollection services)
        {
            services.AddScoped<ILogger, DbLogger>();

            return services;
        }

        /// <summary>
        /// Добавление автомапперов.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeLoggingMappers(this IServiceCollection services)
        {
            services.AddAutoMapper((IMapperConfigurationExpression cfg) =>
            {
                cfg.AddProfile<MyRecipeLoggingMappingProfile>();
            });

            return services;
        }

        /// <summary>
        /// Метод <see cref="Action"/>'а для добавления контекста базы данных MyRecipeLogging в коллекцию сервисов DI.
        /// </summary>
        /// <param name="sp">Провайдер сервисов.</param>
        /// <param name="dbOptions">Опции для построения конекста базы данных.</param>
        private static void AddMyRecipeLoggingDbContext(IServiceProvider sp, DbContextOptionsBuilder dbOptions)
        {
            RegistrarHelper.AddDbContextAction("MyRecipeLoggingDb", sp, dbOptions);
        }
    }
}