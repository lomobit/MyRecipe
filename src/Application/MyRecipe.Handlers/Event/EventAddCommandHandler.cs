using MediatR;
using MyRecipe.Handlers.Contracts.Event;

namespace MyRecipe.Handlers.Event;

public class EventAddCommandHandler : IRequestHandler<EventAddCommand, int>
{
    
    public async Task<int> Handle(EventAddCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}