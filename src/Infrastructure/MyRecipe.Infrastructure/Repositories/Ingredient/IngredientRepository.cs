using Microsoft.EntityFrameworkCore;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Ingredient;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using MyRecipe.Contracts.Enums.Common;
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
        public async Task<Pagination<Domain.Ingredient>> GetAsync(IngredientGetPageQuery request, CancellationToken cancellationToken)
        {
            var commonQuery = _context.Ingredients.AsNoTracking();

            // Добавление фильтрации по имени
            if (!string.IsNullOrEmpty(request.NameFilter))
            {
                // TODO: добавить валидацию текстовых полей в Behaviour для всех запросов, потому что сейчас доступна SQL-инъекция для запросов.
                commonQuery = commonQuery.Where(x => x.Name.ToLower().Contains(request.NameFilter.ToLower()));
            }

            // Добавление сортировки
            var fieldExpression = GetExpressionsForGet(request.SortingField);
            var sliceQuery = AddSortingForGet(commonQuery, request.SortingOrder, fieldExpression);
            
            var ingredientsCount = await commonQuery
                .CountAsync(cancellationToken);
            
            var ingredientSlice = await sliceQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new Pagination<Domain.Ingredient>(ingredientsCount, ingredientSlice);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Domain.Ingredient>> GetAllAsync(
            IngredientGetAllQuery query,
            CancellationToken cancellationToken)
        {
            var commonQuery = _context.Ingredients.AsNoTracking();

            // Добавление фильтрации по имени
            if (!string.IsNullOrEmpty(query.NameFilter))
            {
                // TODO: добавить валидацию текстовых полей в Behaviour для всех запросов, потому что сейчас доступна SQL-инъекция для запросов.
                commonQuery = commonQuery.Where(x => x.Name.ToLower().Contains(query.NameFilter.ToLower()));
            }

            commonQuery = commonQuery.OrderBy(x => x.Name);

            return await commonQuery.ToArrayAsync(cancellationToken);
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

        /// <inheritdoc/>
        public async Task<int[]> GetNonExistsIds(IEnumerable<int> ids, CancellationToken cancellationToken)
        {
            var existsIds = await _context.Ingredients
                .AsNoTracking()
                .Where(i => ids.Contains(i.Id))
                .Select(i => i.Id)
                .ToArrayAsync(cancellationToken);

            return ids.Except(existsIds).ToArray();
        }

        /// <summary>
        /// Возвращает Expression для методов сортировки, в зависимости от используемого для сортировки поля.
        /// </summary>
        /// <param name="sortingField">Используемое для сортировки поле.</param>
        private Expression<Func<Domain.Ingredient, object>> GetExpressionsForGet(SortingFieldEnum sortingField)
        {
            switch (sortingField)
            {
                case SortingFieldEnum.Name:
                    return x => x.Name;
                default:
                    return x => x.Id;
            }
        }

        /// <summary>
        /// Возвращает IQueryable с добавленной сортировкой.
        /// </summary>
        /// <param name="query">Изначальный IQueryable.</param>
        /// <param name="sortingOrder">Порядок сортировки.</param>
        /// <param name="expression">Expression для сортировки.</param>
        private IQueryable<Domain.Ingredient> AddSortingForGet(
            IQueryable<Domain.Ingredient> query,
            SortingOrderEnum sortingOrder,
            Expression<Func<Domain.Ingredient, object>> expression)
        {
            switch (sortingOrder)
            {
                case SortingOrderEnum.Descending:
                    return query.OrderByDescending(expression);
                default:
                    return query.OrderBy(expression);
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
