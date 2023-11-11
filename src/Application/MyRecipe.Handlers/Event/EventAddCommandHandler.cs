using MediatR;
using MyRecipe.AppServices.Event;
using MyRecipe.Handlers.Contracts.Event;

namespace MyRecipe.Handlers.Event;

public class EventAddCommandHandler : IRequestHandler<EventAddCommand, int>
{
    private readonly IEventService _eventService;

    public EventAddCommandHandler(IEventService eventService)
    {
        _eventService = eventService;
    }
    public async Task<int> Handle(EventAddCommand request, CancellationToken cancellationToken)
    {
        return await _eventService.AddAsync(request, cancellationToken);
    }
}