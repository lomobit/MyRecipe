using MediatR;
using MyRecipe.AppServices.Dish;
using MyRecipe.Handlers.Contracts.Dish;

namespace MyRecipe.Handlers.Dish
{
    public class DishAddCommandHandler : IRequestHandler<DishAddCommand, int>
    {
        private readonly IDishService _dishService;

        public DishAddCommandHandler(IDishService dishService)
        {
            _dishService = dishService;
        }

        public async Task<int> Handle(DishAddCommand command, CancellationToken cancellationToken)
        {
            return await _dishService.AddAsync(command, cancellationToken);
        }
    }
}
