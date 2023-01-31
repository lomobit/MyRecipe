
using MyRecipe.Contracts.Ingredient;

namespace MyRecipe.AppServices.Ingredient
{
    public interface IIngredientService
    {
        /// <summary>
        /// Добавление ингридиента в базу данных.
        /// </summary>
        /// <param name="ingredientDto">Данные об ингредиенте.</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор ингридиента.</returns>
        Task<int> AddAsync(IngredientDto ingredientDto, CancellationToken cancellationToken);
    }
}
