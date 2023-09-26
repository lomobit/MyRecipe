
namespace MyRecipe.Handlers.Contracts.Dish
{
    public class IngredientsForDishAddCommand : IIngredientsForDish
    {
        /// <summary>
        /// Идентификатор ингредиента.
        /// </summary>
        public int IngredientId { get; set; }

        /// <summary>
        /// Количество данного ингредиента в блюде.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Единица измерения ингредиента.
        /// </summary>
        public string OkeiCode { get; set; }

        /// <summary>
        /// Состояние ингредиента для блюда.
        /// </summary>
        public string Condition { get; set; }
    }
}
