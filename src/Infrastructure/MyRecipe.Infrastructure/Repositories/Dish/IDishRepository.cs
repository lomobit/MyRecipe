using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Dish;
using MyRecipe.Handlers.Contracts.Dish;

namespace MyRecipe.Infrastructure.Repositories.Dish
{
    public interface IDishRepository
    {
        /// <summary>
        /// Добавление блюда в базу данных.
        /// </summary>
        /// <param name="dish">Данные о блюде.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор добавленного блюда.</returns>
        Task<int> AddAsync(DishDto dish, CancellationToken cancellationToken);

        /// <summary>
        /// Метод получения списка блюд с краткой информацией.
        /// </summary>
        /// <param name="query">Данные запроса.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список блюд с краткой информацией.</returns>
        Task<Pagination<Domain.Dish>> GetListAsync(DishGetPageQuery query, CancellationToken cancellationToken);

        /// <summary>
        /// Получение полной информации о блюде по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор блюда.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о блюде.</returns>
        Task<Domain.Dish?> TryGetAsync(int id, CancellationToken cancellationToken);
    }
}
