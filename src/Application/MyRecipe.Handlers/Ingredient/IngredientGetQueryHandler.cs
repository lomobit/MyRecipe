
using MediatR;
using MyRecipe.AppServices.Ingredient;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Ingredient;

namespace MyRecipe.Handlers.Ingredient
{
    public class IngredientGetQueryHandler : IRequestHandler<IngredientGetQuery, Pagination<IngredientDto>>
    {
        private readonly IIngredientService _ingredientService;

        public IngredientGetQueryHandler(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task<Pagination<IngredientDto>> Handle(IngredientGetQuery request, CancellationToken cancellationToken)
        {
#pragma warning disable CS8629 // Nullable value type may be null.
            return await _ingredientService.GetAsync(request.PageNumber.Value, request.PageSize.Value, cancellationToken);
#pragma warning restore CS8629 // Nullable value type may be null.
        }
    }
}
