namespace MyRecipe.Handlers.Contracts.Dish;

public class IngredientsForDishEditCommand : IIngredientsForDish
{
    /// <summary>
    /// Идентификатор изменяемого ингредиента для блюда.
    /// </summary>
    public int? Id { get; set; }

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