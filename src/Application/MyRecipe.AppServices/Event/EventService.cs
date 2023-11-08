using MyRecipe.Infrastructure.Repositories.Event;

namespace MyRecipe.AppServices.Event;

public class EventService : IEventService
{
    public IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}