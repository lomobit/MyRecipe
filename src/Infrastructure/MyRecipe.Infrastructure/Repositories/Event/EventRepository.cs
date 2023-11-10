using MyRecipe.Contracts.Enums.Meal;
using MyRecipe.Handlers.Contracts.Event;

namespace MyRecipe.Infrastructure.Repositories.Event;

public class EventRepository : IEventRepository
{
    private readonly MyRecipeDbContext _context;

    public EventRepository(MyRecipeDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(EventAddCommand command, CancellationToken cancellationToken)
    {
        Domain.Event @event;
        try
        {
            @event = new Domain.Event
            {
                Name = command.Name,
                Description = command.Description,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                Meals = command.Meals.Select(x => new Domain.Meal
                {
                    Description = x.Description,
                    Date = x.Date,
                    MealType = x.Type,
                    Dishes = _context.Dishes.Where(dish => x.DishesIds.Contains(dish.Id))
                })
            };

            _context.Events.Add(@event);
            await _context.SaveChangesAsync(cancellationToken);
        }
        finally
        {
            _context.ChangeTracker.Clear();
        }

        return @event.Id;
    }
}