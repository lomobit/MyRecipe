using MediatR;
using Microsoft.AspNetCore.Http;

namespace MyRecipe.Handlers.Contracts.Dish
{
    public class DishAddCommand : IRequest<int>
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
        /// Блюдо на какое количество человек.
        /// </summary>
        public int NumberOfPersons { get; set; }
        
        /// <summary>
        /// Файл.
        /// </summary>
        public IFormFile? DishPhoto { get; set; }

        /// <summary>
        /// Список ингредиентов для блюда. Передаётся строкой, т.к. принимаем запросы в формате "multipart/form-data".
        /// </summary>
        public string IngredientsForDish { get; set; }
    }
}
