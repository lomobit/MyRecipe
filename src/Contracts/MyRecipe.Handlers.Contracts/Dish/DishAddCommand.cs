using MediatR;

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
        public string Description { get; set; }

        /// <summary>
        /// Блюдо на какое количество человек.
        /// </summary>
        public int NumberOfPersons { get; set; }

        /// <summary>
        /// Идентификатора загруженного фото блюда.
        /// </summary>
        public Guid? DishPhotoGuid { get; set; }

        /// <summary>
        /// Ингредиенты для блюда.
        /// </summary>
        public List<IngredientsForDishAddCommand> IngredientsForDish { get; set; }
    }
}
