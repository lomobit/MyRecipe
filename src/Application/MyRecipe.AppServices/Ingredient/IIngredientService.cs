
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Ingredient;
using MyRecipe.Handlers.Contracts.Ingredient;

namespace MyRecipe.AppServices.Ingredient
{
    public interface IIngredientService
    {
        /// <summary>
        /// Добавление ингридиента в базу данных.
        /// </summary>
        /// <param name="ingredientDto">Данные об ингредиенте.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор ингридиента.</returns>
        Task<int> AddAsync(IngredientDto ingredientDto, CancellationToken cancellationToken);

        /// <summary>
        /// Получение из базы списка ингредиентов с пейджинацией.
        /// </summary>
        /// <param name="request">Данные запроса.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список ингредиентов для страницы.</returns>
        Task<Pagination<IngredientDto>> GetAsync(IngredientGetQuery request, CancellationToken cancellationToken);

        /// <summary>
        /// Получение из базы списка всех ингредиентов.
        /// </summary>
        /// <param name="query">Данные запроса.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список ингредиентов.</returns>
        Task<IEnumerable<IngredientDto>> GetAllAsync(IngredientGetAllQuery query, CancellationToken cancellationToken);

        /// <summary>
        /// Изменение ингридиента в базе данных.
        /// </summary>
        /// <param name="ingredientDto">Данные об ингредиенте.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Возвращает успешно ли был обновлен ингредиент.</returns>
        Task<bool> EditAsync(IngredientDto ingredientDto, CancellationToken cancellationToken);
    }
}
