using AutoMapper;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Dish;
using MyRecipe.Handlers.Contracts.Dish;
using MyRecipe.Infrastructure.Repositories.Dish;
using MyRecipe.Infrastructure.Repositories.Ingredient;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyRecipe.AppServices.Dish
{
    public class DishService : IDishService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public DishService(
            IIngredientRepository ingredientRepository,
            IDishRepository dishRepository,
            IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(DishAddCommand command, CancellationToken cancellationToken)
        {
            // Валидация существования ингредиентов
            var ingredientsIds = command.IngredientsForDish.Select(x => x.IngredientId);
            var unknownIngredientsIds = await _ingredientRepository.GetNonExistsIds(ingredientsIds, cancellationToken);
            if (unknownIngredientsIds != null && unknownIngredientsIds.Any())
            {
                var unknownIngredientsIdsInRow = string.Join(", ", unknownIngredientsIds);

                var ex = new ValidationException($"Ингредиенты с идентификаторами [{unknownIngredientsIdsInRow}] не существует");
                ex.Data.Add("Ингредиент", $"Ингредиенты с идентификаторами [{unknownIngredientsIdsInRow}] не существует");

                throw ex;
            }

            // TODO: добавить валидацию существования Okei-кодов

            // Добавление нового блюда
            return await _dishRepository.AddAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Pagination<DishForGridDto>> GetListAsync(DishGetListQuery query, CancellationToken cancellationToken)
        {
            var paginatedResult = await _dishRepository.GetListAsync(query, cancellationToken);
            return new Pagination<DishForGridDto>(
                paginatedResult.Count,
                _mapper.Map<IEnumerable<DishForGridDto>>(paginatedResult.ItemsSlice));
        }

        /// <inheritdoc/>
        public async Task<DishDto> GetAsync(int id, CancellationToken cancellationToken)
        {
            var dish = await _dishRepository.TryGetAsync(id, cancellationToken);
            if (dish == null)
            {
                var ex = new ValidationException($"Блюдо с идентификатором \"{id}\" не существует");
                ex.Data.Add("Блюдо", $"Блюдо с идентификатором \"{id}\" не существует");

                throw ex;
            }

            return _mapper.Map<DishDto>(dish);
        }
    }
}
