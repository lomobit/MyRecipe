
using Microsoft.EntityFrameworkCore;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Ingredient;
using System.ComponentModel.DataAnnotations;

namespace MyRecipe.Infrastructure.Repositories.Ingredient
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly MyRecipeDbContext _context;

        public IngredientRepository(MyRecipeDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(IngredientDto ingredientDto, CancellationToken cancellationToken)
        {
            var isAlreadyExist = await _context.Ingredients
                .AsNoTracking()
                .AnyAsync(x => x.Name == ingredientDto.Name, cancellationToken);
            if (isAlreadyExist)
            {
                var ex = new ValidationException($"Ингредиент с названием \"{ingredientDto.Name}\" уже существует");
                ex.Data.Add("Ингредиент", $"Ингредиент с названием \"{ingredientDto.Name}\" уже существует");

                throw ex;
            }

            try
            {
                await _context.Ingredients.AddAsync(new Domain.Ingredient
                {
                    Name = ingredientDto.Name,
                    Description = ingredientDto.Description,
                }, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            finally
            {
                _context.ChangeTracker.Clear();
            }

            return await _context.Ingredients
                .AsNoTracking()
                .Where(x => x.Name == ingredientDto.Name)
                .Select(x => x.Id)
                .FirstAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Pagination<Domain.Ingredient>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var ingredientsCount = await _context.Ingredients
                .AsNoTracking()
                .CountAsync(cancellationToken);
            var ingredientSlice = await _context.Ingredients
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new Pagination<Domain.Ingredient>(pageSize, pageNumber, ingredientsCount, ingredientSlice);
        }
    }
}
