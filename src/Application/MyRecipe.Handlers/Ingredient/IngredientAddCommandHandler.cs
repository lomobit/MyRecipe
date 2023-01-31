using MediatR;
using MyRecipe.AppServices.Ingredient;
using MyRecipe.Contracts.Ingredient;

namespace MyRecipe.Handlers.Ingredient
{
    public class IngredientAddCommandHandler : IRequestHandler<IngredientAddCommand, int>
    {
        private readonly IIngredientService _ingredientService;

        public IngredientAddCommandHandler(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task<int> Handle(IngredientAddCommand request, CancellationToken cancellationToken)
        {
            return await _ingredientService.AddAsync(
                new IngredientDto
                {
                    Name = request.Name,
                    Description = request.Description
                }, cancellationToken);
        }
    }
}
