using MediatR;
using MyRecipe.AppServices.Ingredient;
using MyRecipe.Contracts.Ingredient;

namespace MyRecipe.Handlers.Ingredient
{
    public class IngredientEditCommandHandler : IRequestHandler<IngredientEditCommand, bool>
    {
        private readonly IIngredientService _ingredientService;

        public IngredientEditCommandHandler(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task<bool> Handle(IngredientEditCommand request, CancellationToken cancellationToken)
        {
            return await _ingredientService.EditAsync(
                new IngredientDto
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description
                }, cancellationToken);
        }
    }
}
