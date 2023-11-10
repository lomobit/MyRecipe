using MyRecipe.Contracts.Enums.Meal;

namespace MyRecipe.Domain
{
    public class Meal
    {
        /// <summary>
        /// Идентификатор приёма пищи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Описание приёма пищи.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Дата и время приёма пищи.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Тип приёма пищи.
        /// </summary>
        public MealTypeEnum MealType { get; set; }

        /// <summary>
        /// Блюда приёма пищи.
        /// </summary>
        public virtual IEnumerable<Dish> Dishes { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Meal()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Dishes = Enumerable.Empty<Dish>();
        }
    }
}
