using MediatR;
using MyRecipe.AppServices.Dish;
using MyRecipe.Contracts.Dish;
using MyRecipe.Handlers.Contracts.Dish;

namespace MyRecipe.Handlers.Dish
{
    public class DishGetQueryHandler : IRequestHandler<DishGetQuery, DishDto>
    {
        private readonly IDishService _dishService;

        public DishGetQueryHandler(IDishService dishService)
        {
            _dishService = dishService;
        }

        public async Task<DishDto> Handle(DishGetQuery query, CancellationToken cancellationToken)
        {
            return await _dishService.GetAsync(query.Id, cancellationToken);
        }
    }
}
