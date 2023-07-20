using MediatR;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Enums.Common;
using MyRecipe.Contracts.Ingredient;

namespace MyRecipe.Handlers.Contracts.Ingredient
{
    public class IngredientGetPageQuery : IRequest<Pagination<IngredientDto>>
    {
        /// <summary>
        /// Номер страницы.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Размер страницы.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Порядок сортировки.
        /// </summary>
        public SortingOrderEnum SortingOrder { get; set; } = SortingOrderEnum.Ascending;

        /// <summary>
        /// Поле, по которому происходит сортировка для получения ингредиентов.
        /// </summary>
        public SortingFieldEnum SortingField { get; set; } = SortingFieldEnum.Id;

        /// <summary>
        /// Фильтр по названию ингредиента.
        /// </summary>
        public string? NameFilter { get; set; }
    }
}
