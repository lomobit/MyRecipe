using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Event;
using MyRecipe.Handlers.Contracts.Event;
using MyRecipe.Infrastructure.Repositories.Event;

namespace MyRecipe.AppServices.Event;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    /// <inheritdoc/>
    public async Task<int> AddAsync(EventAddCommand command, CancellationToken cancellationToken)
    {
        return await _eventRepository.AddAsync(command, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Pagination<EventForGridDto>> GetPageAsync(EventGetPageQuery query, CancellationToken cancellationToken)
    {
        return await _eventRepository.GetPageAsync(query, cancellationToken);
    }
}