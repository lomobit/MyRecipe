
using MediatR;
using MyRecipe.AppServices.Ingredient;
using MyRecipe.Contracts.Ingredient;

namespace MyRecipe.Handlers.Ingredient
{
    public class IngredientGetQueryHandler : IRequestHandler<IngredientGetQuery, IEnumerable<IngredientDto>>
    {
        private readonly IIngredientService _ingredientService;

        public IngredientGetQueryHandler(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task<IEnumerable<IngredientDto>> Handle(IngredientGetQuery request, CancellationToken cancellationToken)
        {
            return await _ingredientService.GetAsync(request.PageNumber.Value, request.PageSize.Value, cancellationToken);
        }
    }
}
