namespace MyRecipe.Infrastructure.Repositories.Event;

public class EventRepository : IEventRepository
{
    private readonly MyRecipeDbContext _context;

    public EventRepository(MyRecipeDbContext context)
    {
        _context = context;
    }
}