using MediatR;
using MyRecipe.Contracts.Ingredient;

namespace MyRecipe.Handlers.Contracts.Ingredient;

public class IngredientGetAllQuery : IRequest<IEnumerable<IngredientDto>>
{
    /// <summary>
    /// Фильтр по названию ингредиента.
    /// </summary>
    public string? NameFilter { get; set; }
}