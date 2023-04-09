
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Ingredient;
using MyRecipe.Handlers.Contracts.Ingredient;

namespace MyRecipe.Infrastructure.Repositories.Ingredient
{
    public interface IIngredientRepository
    {
        /// <summary>
        /// Добавление ингридиента в базу данных.
        /// </summary>
        /// <param name="ingredientDto">Данные об ингредиенте.</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор ингридиента.</returns>
        Task<int> AddAsync(IngredientDto ingredientDto, CancellationToken cancellationToken);

        /// <summary>
        /// Получение из базы списка ингредиентов с пейджинацией.
        /// </summary>
        /// <param name="request">Данные запроса.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список ингредиентов для страницы.</returns>
        Task<Pagination<Domain.Ingredient>> GetAsync(IngredientGetQuery request, CancellationToken cancellationToken);

        /// <summary>
        /// Изменяет ингредиент в базе данных.
        /// </summary>
        /// <param name="ingredientDto">Данные об ингредиенте.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Возвращает успешно ли был обновлен ингредиент.</returns>
        Task<bool> EditAsync(IngredientDto ingredientDto, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить какие идентификаторы ингредиентов не существуют.
        /// </summary>
        /// <param name="ids">Идентификаторы ингредиентов.</param>
        /// /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Переданные идентификаторы, которые не существуют.</returns>
        Task<int[]> CheckWhichIdsDontExist(IEnumerable<int> ids, CancellationToken cancellationToken);
    }
}
