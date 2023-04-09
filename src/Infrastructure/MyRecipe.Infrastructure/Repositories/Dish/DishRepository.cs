using Microsoft.EntityFrameworkCore;
using MyRecipe.Contracts.Ingredient;
using MyRecipe.Domain;
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
                // Добавляем ингредиенты, указанные для блюда
                var ingredientsForDish = new List<IngredientsForDish>(command.IngredientsForDish.Count);
                foreach (var ingredient in command.IngredientsForDish)
                {
                    ingredientsForDish.Add(new IngredientsForDish
                    {
                        IngredientId = ingredient.IngredientId,
                        Quantity = ingredient.Quantity,
                        OkeiCode = ingredient.OkeiCode,
                        Condition = ingredient.Condition,
                    });
                }

                // Добавляем новое блюдо
                var dish = new Domain.Dish
                {
                    Name = command.Name,
                    NumberOfPersons = command.NumberOfPersons,
                    Description = command.Description,
                    DishPhotoGuid = command.DishPhotoGuid,
                    IngredientsForDish = ingredientsForDish
                };

                await _context.Dishes.AddAsync(dish, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            finally
            {
                _context.ChangeTracker.Clear();
            }

            return await _context.Dishes
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
