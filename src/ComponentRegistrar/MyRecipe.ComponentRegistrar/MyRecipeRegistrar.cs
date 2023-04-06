﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipe.AppServices.Ingredient;
using MyRecipe.Common.ComponentRegistrar;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Ingredient;
using MyRecipe.Handlers.Contracts.Ingredient;
using MyRecipe.Handlers.Ingredient;
using MyRecipe.Infrastructure;
using MyRecipe.Infrastructure.MappingProfiles;
using MyRecipe.Infrastructure.Repositories.Ingredient;

namespace MyRecipe.ComponentRegistrar
{
    public static class MyRecipeRegistrar
    {
        /// <summary>
        /// Добавление зависимостей для MyRecipe.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipe(this IServiceCollection services)
        {
            // Добавление DbContext'ов
            services.AddMyRecipeDbContexts();

            // Добавление сервисов приложения
            services.AddMyRecipeServices();

            // Добавление репозиториев для работы с базой данных
            services.AddMyRecipeRepositories();

            // Добавление автомапперов.
            services.AddMyRecipeMappers();

            // Добавление поведенческих пайплайнов.
            services.AddMyRecipePipelineBehaviors();

            return services;
        }

        /// <summary>
        /// Добавление DbContext'ов.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeDbContexts(this IServiceCollection services)
        {
            services.AddDbContext<MyRecipeDbContext>(AddMyRecipeDbContext, ServiceLifetime.Scoped);

            return services;
        }

        /// <summary>
        /// Добавление сервисов приложения.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeServices(this IServiceCollection services)
        {
            services.AddScoped<IIngredientService, IngredientService>();

            return services;
        }

        /// <summary>
        /// Добавление репозиториев для работы с базой данных.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeRepositories(this IServiceCollection services)
        {
            // Репозитории MyRecipeDbContext'а
            services.AddScoped<IIngredientRepository, IngredientRepository>();

            return services;
        }

        /// <summary>
        /// Добавление автомапперов.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipeMappers(this IServiceCollection services)
        {
            services.AddAutoMapper((IMapperConfigurationExpression cfg) =>
            {
                cfg.AddProfile<MyRecipeMappingProfile>();
            });

            return services;
        }

        /// <summary>
        /// Добавление поведенческих пайплайнов.
        /// </summary>
        /// <param name="services">Коллекция сервисов DI.</param>
        /// <returns>Коллекция сервисов DI.</returns>
        public static IServiceCollection AddMyRecipePipelineBehaviors(this IServiceCollection services)
        {
            services.AddScoped<IPipelineBehavior<IngredientGetQuery, Pagination<IngredientDto>>, IngredientGetQueryBehavior>();

            return services;
        }

        /// <summary>
        /// Метод <see cref="Action"/>'а для добавления контекста базы данных MyRecipe в коллекцию сервисов DI.
        /// </summary>
        /// <param name="sp">Провайдер сервисов.</param>
        /// <param name="dbOptions">Опции для построения конекста базы данных.</param>
        private static void AddMyRecipeDbContext(IServiceProvider sp, DbContextOptionsBuilder dbOptions)
        {
            RegistrarHelper.AddDbContextAction("MyRecipeDb", sp, dbOptions);
        }
    }
}