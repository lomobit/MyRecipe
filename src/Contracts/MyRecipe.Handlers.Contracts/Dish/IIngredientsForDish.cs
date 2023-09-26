namespace MyRecipe.Handlers.Contracts.Dish;

public interface IIngredientsForDish
{
    /// <summary>
    /// Идентификатор ингредиента.
    /// </summary>
    public int IngredientId { get; set; }
    
    /// <summary>
    /// Единица измерения ингредиента.
    /// </summary>
    public string OkeiCode { get; set; }
}