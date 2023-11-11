using MediatR;
using MyRecipe.AppServices.Event;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Event;
using MyRecipe.Handlers.Contracts.Event;

namespace MyRecipe.Handlers.Event;

public class EventGetPageQueryHandler: IRequestHandler<EventGetPageQuery, Pagination<EventForGridDto>>
{
    private readonly IEventService _eventService;

    public EventGetPageQueryHandler(IEventService eventService)
    {
        _eventService = eventService;
    }

    public async Task<Pagination<EventForGridDto>> Handle(EventGetPageQuery request, CancellationToken cancellationToken)
    {
        return await _eventService.GetPageAsync(request, cancellationToken);
    }
}