using MediatR;
using MyRecipe.AppServices.Dish;
using MyRecipe.Handlers.Contracts.Dish;

namespace MyRecipe.Handlers.Dish;

public class DishEditCommandHandler : IRequestHandler<DishEditCommand, bool>
{
    private readonly IDishService _dishService;

    public DishEditCommandHandler(IDishService dishService)
    {
        _dishService = dishService;
    }

    public async Task<bool> Handle(DishEditCommand command, CancellationToken cancellationToken)
    {
        return await _dishService.EditAsync(command, cancellationToken);
    }
}