using MediatR;
using MyRecipe.Contracts.Dish;

namespace MyRecipe.Handlers.Contracts.Dish
{
    public class DishGetQuery : IRequest<DishDto>
    {
        /// <summary>
        /// Идентификатор запрашиваемого блюда.
        /// </summary>
        public int Id { get; set; }

        public DishGetQuery(int id)
        {
            Id = id;
        }
    }
}
