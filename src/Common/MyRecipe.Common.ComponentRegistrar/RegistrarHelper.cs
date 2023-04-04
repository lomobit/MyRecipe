using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyRecipe.Common.ComponentRegistrar
{
    public static class RegistrarHelper
    {
        /// <summary>
        /// Метод <see cref="Action"/>'а для добавления контекста базы данных в коллекцию сервисов DI.
        /// </summary>
        /// <param name="connectionStringName">Название строки подключения.</param>
        /// <param name="sp">Провайдер сервисов.</param>
        /// <param name="dbOptions">Опции для построения конекста базы данных.</param>
        /// <exception cref="InvalidOperationException">Не найдена строка подключения.</exception>
        public static void AddDbContextAction(string connectionStringName, IServiceProvider sp, DbContextOptionsBuilder dbOptions)
        {
            var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString(connectionStringName);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException($"Не найдена строка подключения с именем {connectionStringName}");
            }

            dbOptions
                .UseLazyLoadingProxies()
                .UseNpgsql(connectionString);
        }
    }
}
