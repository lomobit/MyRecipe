
using MyRecipe.Contracts.Ingredient;
using MyRecipe.Infrastructure.Repositories.Ingredient;

namespace MyRecipe.AppServices.Ingredient
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(IngredientDto ingredientDto, CancellationToken cancellationToken)
        {
            return await _ingredientRepository.AddAsync(ingredientDto, cancellationToken);
        }
    }
}
