using MediatR;

namespace MyRecipe.Handlers.Contracts.Event;

public class EventAddCommand : IRequest<int>
{
    /// <summary>
    /// Название блюда.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание блюда.
    /// </summary>
    public string? Description { get; set; }
}