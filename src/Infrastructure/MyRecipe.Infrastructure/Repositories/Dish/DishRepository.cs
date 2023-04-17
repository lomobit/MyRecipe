using Microsoft.EntityFrameworkCore;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Enums.Common;
using MyRecipe.Domain;
using MyRecipe.Handlers.Contracts.Dish;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

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

            // Т.к. имена у блюд уникальные, то здесь можем искать идентификатор нашего блюда по имени.
            return await _context.Dishes
                .AsNoTracking()
                .Where(x => x.Name == command.Name)
                .Select(x => x.Id)
                .FirstAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Pagination<Domain.Dish>> GetListAsync(DishGetListQuery query, CancellationToken cancellationToken)
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
        //       Подумать как их вынести в одно место.

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
