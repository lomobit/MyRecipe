
using MediatR;
using MyRecipe.Contracts.Ingredient;

namespace MyRecipe.Handlers.Ingredient
{
    public class IngredientGetQuery : IRequest<IEnumerable<IngredientDto>>
    {
        /// <summary>
        /// Номер страницы.
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Размер страницы.
        /// </summary>
        public int? PageSize { get; set; } = 25;
    }
}
