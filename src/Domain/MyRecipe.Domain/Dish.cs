
namespace MyRecipe.Domain
{
    public class Dish
    {
        /// <summary>
        /// Идентификатор блюда.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование блюда.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// На какое количество человек
        /// </summary>
        public int NumberOfPersons { get; set; }

        /// <summary>
        /// Список ингредиентов для блюда.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual List<IngredientsForDish> IngredientsForDish { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
