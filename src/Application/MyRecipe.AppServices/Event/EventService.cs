using MyRecipe.Handlers.Contracts.Event;
using MyRecipe.Infrastructure.Repositories.Event;

namespace MyRecipe.AppServices.Event;

public class EventService : IEventService
{
    public IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<int> AddAsync(EventAddCommand command, CancellationToken cancellationToken)
    {
        return await _eventRepository.AddAsync(command, cancellationToken);
    }
}