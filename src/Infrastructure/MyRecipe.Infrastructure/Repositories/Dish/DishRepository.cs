using Microsoft.EntityFrameworkCore;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Enums.Common;
using MyRecipe.Domain;
using MyRecipe.Handlers.Contracts.Dish;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using MyRecipe.Contracts.Dish;

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
        public async Task<int> AddAsync(DishDto dishDto, CancellationToken cancellationToken)
        {
            await ValidateName(dishDto.Name, cancellationToken);

            try
            {
                // Добавляем новое блюдо
                var dish = new Domain.Dish
                {
                    Name = dishDto.Name,
                    NumberOfPersons = dishDto.NumberOfPersons,
                    Description = dishDto.Description,
                    DishPhotoGuid = dishDto.DishPhotoGuid,
                    IngredientsForDish = dishDto.IngredientsForDish.Select(x => new IngredientsForDish
                    {
                        IngredientId = x.IngredientId,
                        Quantity = x.Quantity,
                        OkeiCode = x.OkeiCode,
                        Condition = x.Condition
                    }).ToList()
                };

                _context.Dishes.Add(dish);
                await _context.SaveChangesAsync(cancellationToken);
            }
            finally
            {
                _context.ChangeTracker.Clear();
            }

            // Т.к. имена у блюд уникальные, то здесь можем искать идентификатор нашего блюда по имени.
            return await _context.Dishes
                .AsNoTracking()
                .Where(x => x.Name == dishDto.Name)
                .Select(x => x.Id)
                .FirstAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<bool> EditAsync(DishDto dishDto, CancellationToken cancellationToken)
        {
            var currentDishVersion = await _context.Dishes.FirstOrDefaultAsync(x => x.Id == dishDto.Id, cancellationToken);
            if (currentDishVersion == null)
            {
                var ex = new ValidationException($"Блюдо с идентификатором \"{dishDto.Id}\" не существует");
                ex.Data.Add("Блюдо", $"Блюдо с идентификатором \"{dishDto.Id}\" не существует");

                throw ex;
            }

            if (dishDto.Name != currentDishVersion.Name)
            {
                await ValidateName(dishDto.Name, cancellationToken);
            }

            try
            {
                currentDishVersion.Name = dishDto.Name;
                currentDishVersion.NumberOfPersons = dishDto.NumberOfPersons;
                currentDishVersion.Description = dishDto.Description;
                currentDishVersion.DishPhotoGuid = dishDto.DishPhotoGuid;

                currentDishVersion.IngredientsForDish.Clear();
                currentDishVersion.IngredientsForDish = dishDto.IngredientsForDish.Select(x => new IngredientsForDish
                {
                    Id = x.Id,
                    IngredientId = x.IngredientId,
                    Quantity = x.Quantity,
                    OkeiCode = x.OkeiCode,
                    Condition = x.Condition
                }).ToList();

                await _context.SaveChangesAsync(cancellationToken);
            }
            finally
            {
                _context.ChangeTracker.Clear();
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<Pagination<Domain.Dish>> GetListAsync(DishGetPageQuery query, CancellationToken cancellationToken)
        {
            var commonQuery = _context.Dishes.AsNoTracking();

            // Добавление фильтрации по имени
            if (!string.IsNullOrEmpty(query.NameFilter))
            {
                // TODO: добавить валидацию текстовых полей в Behaviour для всех запросов, потому что сейчас доступна SQL-инъекция для запросов.
                commonQuery = commonQuery.Where(x => x.Name.ToLower().Contains(query.NameFilter.ToLower()));
            }

            // Добавление сортировки
            var fieldExpression = GetExpressionsForGetList(query.SortingField);
            var sliceQuery = AddSortingForGetList(commonQuery, query.SortingOrder, fieldExpression);

            var ingredientsCount = await commonQuery
                .CountAsync(cancellationToken);

            var ingredientSlice = await sliceQuery
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            return new Pagination<Domain.Dish>(ingredientsCount, ingredientSlice);
        }

        /// <inheritdoc/>
        public async Task<Domain.Dish?> TryGetAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Dishes
                .AsNoTracking()
                .Include(x => x.IngredientsForDish).ThenInclude(x => x.Ingredient)
                .Include(x => x.IngredientsForDish).ThenInclude(x => x.Okei)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        // TODO: Приватные методы практически полностью повторяют методы из репозитория ингредиентов.
        //       Подумать о том, чтобы вынести их в одно место.

        /// <summary>
        /// Возвращает Expression для методов сортировки, в зависимости от используемого для сортировки поля.
        /// </summary>
        /// <param name="sortingField">Используемое для сортировки поле.</param>
        private Expression<Func<Domain.Dish, object>> GetExpressionsForGetList(SortingFieldEnum sortingField)
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
        private IQueryable<Domain.Dish> AddSortingForGetList(
            IQueryable<Domain.Dish> query,
            SortingOrderEnum sortingOrder,
            Expression<Func<Domain.Dish, object>> expression)
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
