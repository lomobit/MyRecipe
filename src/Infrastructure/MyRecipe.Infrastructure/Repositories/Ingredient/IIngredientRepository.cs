
using MyRecipe.Contracts.Ingredient;

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
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="pageSize">Количество записей на странице.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список ингредиентов для страницы.</returns>
        Task<IEnumerable<Domain.Ingredient>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
