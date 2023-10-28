
namespace MyRecipe.Domain
{
    /// <summary>
    /// Расписание приёмов пищи.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Идентификатор расписания приёмов пищи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название расписания приёмов пищи.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание расписания приёмов пищи.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Приёмы пищи.
        /// </summary>
        public virtual IEnumerable<Meal> Meals { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Event()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Meals = Enumerable.Empty<Meal>();
        }
    }
}
