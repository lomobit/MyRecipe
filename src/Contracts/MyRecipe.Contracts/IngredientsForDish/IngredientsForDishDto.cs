using MyRecipe.Contracts.Ingredient;
using MyRecipe.Contracts.Okei;

namespace MyRecipe.Contracts.IngredientsForDish
{
    public class IngredientsForDishDto
    {
        /// <summary>
        /// Идентификатор записи ингредиента для блюда
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор ингредиента
        /// </summary>
        public int IngredientId { get; set; }

        /// <summary>
        /// Ингредиент
        /// </summary>
        public IngredientDto? Ingredient { get; set; }

        /// <summary>
        /// Количество ингредиента для блюда
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Код ОКЕИ (код единицы измерения)
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string OkeiCode { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// ОКЕИ (единица измерения)
        /// </summary>
        public OkeiDto? Okei { get; set; }

        /// <summary>
        /// Состояние ингридиента в блюде
        /// </summary>
        public string? Condition { get; set; }
    }
}
