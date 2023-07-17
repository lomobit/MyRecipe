using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipe.Common.ComponentRegistrar;
using MyRecipeFiles.AppServices.File;
using MyRecipeFiles.Infrastructure;
using MyRecipeFiles.Infrastructure.MappingProfiles;
using MyRecipeFiles.Infrastructure.Repositories.File;

namespace MyRecipeFiles.ComponentRegistrar
{
    public static class MyRecipeFilesRegistrar
    {
        /// <summary>
        /// Добавление зависимостей для MyRecipeFiles.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeFiles(this IServiceCollection services)
        {
            // Добавление DbContext'ов
            services.AddMyRecipeFilesDbContexts();

            // Добавление сервисов приложения
            services.AddMyRecipeFilesServices();

            // Добавление репозиториев для работы с базой данных
            services.AddMyRecipeFilesRepositories();

            // Добавление автомапперов
            services.AddMyRecipeFilesMappers();

            // Добавление поведенческих пайплайнов.
            services.AddMyRecipeFilesPipelineBehaviors();

            return services;
        }

        /// <summary>
        /// Добавление DbContext'ов.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeFilesDbContexts(this IServiceCollection services)
        {
            services.AddDbContext<MyRecipeFilesDbContext>(AddMyRecipeFilesDbContext, ServiceLifetime.Scoped);

            return services;
        }

        /// <summary>
        /// Добавление сервисов приложения.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeFilesServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();

            return services;
        }

        /// <summary>
        /// Добавление репозиториев для работы с базой данных.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeFilesRepositories(this IServiceCollection services)
        {
            // Репозитории MyRecipeFilesDbContext'а
            services.AddScoped<IFileRepository, FileRepository>();

            return services;
        }
        
        /// <summary>
        /// Добавление автомапперов.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeFilesMappers(this IServiceCollection services)
        {
            services.AddAutoMapper((IMapperConfigurationExpression cfg) =>
            {
                cfg.AddProfile<MyRecipeFilesMappingProfile>();
            });

            return services;
        }

        /// <summary>
        /// Добавление поведенческих пайплайнов.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeFilesPipelineBehaviors(this IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        /// Метод <see cref="Action"/>'а для добавления контекста базы данных MyRecipeFiles в коллекцию сервисов DI.
        /// </summary>
        /// <param name="sp">Провайдер сервисов.</param>
        /// <param name="dbOptions">Опции для построения конекста базы данных.</param>
        private static void AddMyRecipeFilesDbContext(IServiceProvider sp, DbContextOptionsBuilder dbOptions)
        {
            RegistrarHelper.AddDbContextAction("MyRecipeFilesDb", sp, dbOptions);
        }
    }
}