using MediatR;
using MyRecipe.AppServices.Dish;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Dish;
using MyRecipe.Handlers.Contracts.Dish;

namespace MyRecipe.Handlers.Dish
{
    public class DishGetPageQueryHandler : IRequestHandler<DishGetPageQuery, Pagination<DishForGridDto>>
    {
        private readonly IDishService _dishService;

        public DishGetPageQueryHandler(IDishService dishService)
        {
            _dishService = dishService;
        }

        public async Task<Pagination<DishForGridDto>> Handle(DishGetPageQuery query, CancellationToken cancellationToken)
        {
            return await _dishService.GetPageAsync(query, cancellationToken);
        }
    }
}
