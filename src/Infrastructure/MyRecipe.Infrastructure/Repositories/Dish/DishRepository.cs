using Microsoft.EntityFrameworkCore;
using MyRecipe.Contracts.Ingredient;
using MyRecipe.Handlers.Contracts.Dish;
using System.ComponentModel.DataAnnotations;

namespace MyRecipe.Infrastructure.Repositories.Dish
{
    public class DishRepository : IDishRepository
    {
        private readonly MyRecipeDbContext _context;

        public DishRepository(MyRecipeDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(DishAddCommand command, CancellationToken cancellationToken)
        {
            await ValidateName(command.Name, cancellationToken);

            try
            {
                await _context.Dishes.AddAsync(new Domain.Dish
                {
                    Name = command.Name,
                    NumberOfPersons = command.NumberOfPersons,

                }, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            finally
            {
                _context.ChangeTracker.Clear();
            }

            return await _context.Ingredients
            .AsNoTracking()
                .Where(x => x.Name == command.Name)
                .Select(x => x.Id)
                .FirstAsync(cancellationToken);
        }

        /// <summary>
        /// Если блюдо с заданным названием уже существует, то выбрасывает исключение.
        /// </summary>
        /// <param name="name">Название блюда.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        private async Task ValidateName(string name, CancellationToken cancellationToken)
        {
            var isNameAlreadyExist = await _context.Dishes
                .AsNoTracking()
                .AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
            if (isNameAlreadyExist)
            {
                var ex = new ValidationException($"Блюдо с названием \"{name}\" уже существует");
                ex.Data.Add("Блюдо", $"Блюдо с названием \"{name}\" уже существует");

                throw ex;
            }
        }
    }
}
