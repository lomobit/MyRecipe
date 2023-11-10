using MyRecipe.Contracts.Enums.Meal;

namespace MyRecipe.Handlers.Contracts.Event;

public class MealDto
{
    /// <summary>
    /// Описание приёма пищи.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Дата приёма пищи.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Тип приёма пищи.
    /// </summary>
    public MealTypeEnum Type { get; set; }

    /// <summary>
    /// Идентификаторы блюд.
    /// </summary>
    public int[] DishesIds { get; set; }
}