using MediatR;
using MyRecipe.AppServices.Ingredient;
using MyRecipe.Contracts.Ingredient;
using MyRecipe.Handlers.Contracts.Ingredient;

namespace MyRecipe.Handlers.Ingredient;

public class IngredientGetAllQueryHandler : IRequestHandler<IngredientGetAllQuery, IEnumerable<IngredientDto>>
{
    private readonly IIngredientService _ingredientService;

    public IngredientGetAllQueryHandler(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    public async Task<IEnumerable<IngredientDto>> Handle(IngredientGetAllQuery query, CancellationToken cancellationToken)
    {
        return await _ingredientService.GetAllAsync(query, cancellationToken);
    }
}