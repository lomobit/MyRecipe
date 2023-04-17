using MediatR;
using MyRecipe.AppServices.Dish;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Dish;
using MyRecipe.Handlers.Contracts.Dish;

namespace MyRecipe.Handlers.Dish
{
    public class DishGetListQueryHandler : IRequestHandler<DishGetListQuery, Pagination<DishForGridDto>>
    {
        private readonly IDishService _dishService;

        public DishGetListQueryHandler(IDishService dishService)
        {
            _dishService = dishService;
        }

        public async Task<Pagination<DishForGridDto>> Handle(DishGetListQuery query, CancellationToken cancellationToken)
        {
            return await _dishService.GetListAsync(query, cancellationToken);
        }
    }
}
