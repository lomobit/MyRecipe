using MyRecipe.Handlers.Contracts.Dish;
using MyRecipe.Infrastructure.Repositories.Dish;

namespace MyRecipe.AppServices.Dish
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _repository;
        public DishService(IDishRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(DishAddCommand command, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(command, cancellationToken);
        }
    }
}
