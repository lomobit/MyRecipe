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

    /// <summary>
    /// Дата начала события.
    /// </summary>
    public DateTimeOffset StartDate { get; set; }
    
    /// <summary>
    /// Дата окончания события.
    /// </summary>
    public DateTimeOffset EndDate { get; set; }

    /// <summary>
    /// Приёмы пищи.
    /// </summary>
    public MealDto[] Meals { get; set; }
}