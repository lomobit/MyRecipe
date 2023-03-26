using Microsoft.EntityFrameworkCore;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Ingredient;
using System.ComponentModel.DataAnnotations;
using MyRecipe.Handlers.Contracts.Ingredient;

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
            await ValidateName(ingredientDto.Name, cancellationToken);

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
        public async Task<Pagination<Domain.Ingredient>> GetAsync(IngredientGetQuery request, CancellationToken cancellationToken)
        {
            var ingredientsCount = await _context.Ingredients
                .AsNoTracking()
                .CountAsync(cancellationToken);
            var ingredientSlice = await _context.Ingredients
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new Pagination<Domain.Ingredient>(ingredientsCount, ingredientSlice);
        }

        /// <inheritdoc/>
        public async Task<bool> EditAsync(IngredientDto ingredientDto, CancellationToken cancellationToken)
        {
            await ValidateId(ingredientDto.Id, cancellationToken);
            await ValidateNameExceptWithId(ingredientDto.Id, ingredientDto.Name, cancellationToken);

            try
            {
                var ingredient = await _context.Ingredients
                    .Where(x => x.Id == ingredientDto.Id)
                    .FirstAsync(cancellationToken);
                
                ingredient.Name = ingredientDto.Name;
                ingredient.Description = ingredientDto.Description;

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                var str = ex.Message;
                throw;
            }
            finally
            {
                _context.ChangeTracker.Clear();
            }
        }

        /// <summary>
        /// Если ингредиента с заданным идентификатором не существует, то выбрасывает исключение.
        /// </summary>
        /// <param name="id">Идентификатор ингредиента.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        private async Task ValidateId(int id, CancellationToken cancellationToken)
        {
            var idIdExist = await _context.Ingredients
                .AsNoTracking()
                .AnyAsync(x => x.Id == id, cancellationToken);

            if (!idIdExist)
            {
                var ex = new ValidationException($"Ингредиент с идентификатором \"{id}\" не существует");
                ex.Data.Add("Ингредиент", $"Ингредиент с идентификатором \"{id}\" не существует");

                throw ex;
            }
        }

        /// <summary>
        /// Если ингредиент с заданным названием уже существует, то выбрасывает исключение.
        /// </summary>
        /// <param name="name">Название ингредиента.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        private async Task ValidateName(string name, CancellationToken cancellationToken)
        {
            var isNameAlreadyExist = await _context.Ingredients
                .AsNoTracking()
                .AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
            if (isNameAlreadyExist)
            {
                var ex = new ValidationException($"Ингредиент с названием \"{name}\" уже существует");
                ex.Data.Add("Ингредиент", $"Ингредиент с названием \"{name}\" уже существует");

                throw ex;
            }
        }

        /// <summary>
        /// Если ингредиент с заданным названием уже существует (не считая указанный идетификатор), то выбрасывает исключение.
        /// </summary>
        /// <param name="id">Идентификатор исключаемого ингредиента.</param>
        /// <param name="name">Название ингредиента.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        private async Task ValidateNameExceptWithId(int id, string name, CancellationToken cancellationToken)
        {
            var isNameAlreadyExist = await _context.Ingredients
                .AsNoTracking()
                .AnyAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != id, cancellationToken);
            if (isNameAlreadyExist)
            {
                var ex = new ValidationException($"Ингредиент с названием \"{name}\" уже существует");
                ex.Data.Add("Ингредиент", $"Ингредиент с названием \"{name}\" уже существует");

                throw ex;
            }
        }
    }
}
