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
    }
}
