using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Dish;
using MyRecipe.Handlers.Contracts.Dish;

namespace MyRecipe.AppServices.Dish
{
    public interface IDishService
    {
        /// <summary>
        /// Добавление блюда в базу данных.
        /// </summary>
        /// <param name="command">Данные о блюде.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор добавленного блюда.</returns>
        Task<int> AddAsync(DishAddCommand command, CancellationToken cancellationToken);
        
        /// <summary>
        /// Изменение блюда в базе данных.
        /// </summary>
        /// <param name="command">Измененные данные о блюде.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Успешность изменения блюда.</returns>
        Task<bool> EditAsync(DishEditCommand command, CancellationToken cancellationToken);

        /// <summary>
        /// Метод получения списка блюд с краткой информацией.
        /// </summary>
        /// <param name="query">Данные запроса.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список блюд с краткой информацией.</returns>
        Task<Pagination<DishForGridDto>> GetListAsync(DishGetPageQuery query, CancellationToken cancellationToken);

        /// <summary>
        /// Получение полной информации о блюде по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор блюда.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о блюде.</returns>
        Task<DishDto> GetAsync(int id, CancellationToken cancellationToken);
    }
}
