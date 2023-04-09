using MyRecipe.Handlers.Contracts.Dish;
using MyRecipe.Infrastructure.Repositories.Dish;
using MyRecipe.Infrastructure.Repositories.Ingredient;
using System.ComponentModel.DataAnnotations;

namespace MyRecipe.AppServices.Dish
{
    public class DishService : IDishService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IDishRepository _dishRepository;

        public DishService(IIngredientRepository ingredientRepository, IDishRepository dishRepository)
        {
            _ingredientRepository = ingredientRepository;
            _dishRepository = dishRepository;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(DishAddCommand command, CancellationToken cancellationToken)
        {
            var ingredientsIds = command.IngredientsForDish.Select(x => x.IngredientId);
            var unknownIngredientsIds = await _ingredientRepository.CheckWhichIdsDontExist(ingredientsIds, cancellationToken);
            if (unknownIngredientsIds != null && unknownIngredientsIds.Any())
            {
                var unknownIngredientsIdsInRow = string.Join(", ", unknownIngredientsIds);

                var ex = new ValidationException($"Ингредиенты с идентификаторами [{unknownIngredientsIdsInRow}] не существует");
                ex.Data.Add("Ингредиент", $"Ингредиенты с идентификаторами [{unknownIngredientsIdsInRow}] не существует");

                throw ex;
            }

            return await _dishRepository.AddAsync(command, cancellationToken);
        }
    }
}
