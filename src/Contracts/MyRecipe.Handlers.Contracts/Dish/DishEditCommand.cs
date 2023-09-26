using MediatR;
using Microsoft.AspNetCore.Http;

namespace MyRecipe.Handlers.Contracts.Dish;

public class DishEditCommand : IRequest<bool>
{
    /// <summary>
    /// Идентификатор изменяемого блюда.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название блюда.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание блюда.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Блюдо на какое количество человек.
    /// </summary>
    public int NumberOfPersons { get; set; }
        
    /// <summary>
    /// Файл.
    /// </summary>
    public IFormFile? DishPhoto { get; set; }

    /// <summary>
    /// Список ингредиентов для блюда.
    /// </summary>
    public IngredientsForDishEditCommand[] IngredientsForDish { get; set; }
}