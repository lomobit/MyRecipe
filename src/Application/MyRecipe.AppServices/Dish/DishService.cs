using AutoMapper;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Dish;
using MyRecipe.Handlers.Contracts.Dish;
using MyRecipe.Infrastructure.Repositories.Dish;
using MyRecipe.Infrastructure.Repositories.Ingredient;
using System.ComponentModel.DataAnnotations;
using MyRecipe.Contracts.IngredientsForDish;
using MyRecipe.Infrastructure.Repositories.Okei;
using MyRecipeFiles.AppServices.File;

namespace MyRecipe.AppServices.Dish
{
    public class DishService : IDishService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IOkeiRepository _okeiRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public DishService(
            IIngredientRepository ingredientRepository,
            IOkeiRepository okeiRepository,
            IDishRepository dishRepository,
            IFileService fileService,
            IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _okeiRepository = okeiRepository;
            _dishRepository = dishRepository;
            _fileService = fileService;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(DishAddCommand command, CancellationToken cancellationToken)
        {
            await ValidateIngredients(command.IngredientsForDish, cancellationToken);
            
            // Добавление файла фотографии блюда в сервис документов.
            var dishPhotoGuid = default(Guid?);
            if (command.DishPhoto is not null)
            {
                dishPhotoGuid = await _fileService.UploadAsync(command.DishPhoto, cancellationToken);
            }

            // Добавление нового блюда
            var dishDto = new DishDto
            {
                Id = 0,
                Name = command.Name,
                NumberOfPersons = command.NumberOfPersons,
                Description = command.Description,
                DishPhotoGuid = dishPhotoGuid,
                IngredientsForDish = command.IngredientsForDish.Select(x => new IngredientsForDishDto
                {
                    IngredientId = x.IngredientId,
                    Quantity = x.Quantity,
                    OkeiCode = x.OkeiCode,
                    Condition = x.Condition
                }).ToList()
            };
            
            return await _dishRepository.AddAsync(dishDto, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<bool> EditAsync(DishEditCommand command, CancellationToken cancellationToken)
        {
            await ValidateIngredients(command.IngredientsForDish, cancellationToken);
            
            // Добавление файла фотографии блюда в сервис документов.
            var dishPhotoGuid = default(Guid?);
            if (command.DishPhoto is not null)
            {
                dishPhotoGuid = await _fileService.UploadAsync(command.DishPhoto, cancellationToken);
            }
            
            // Изменение блюда
            var dishDto = new DishDto
            {
                Id = command.Id,
                Name = command.Name,
                NumberOfPersons = command.NumberOfPersons,
                Description = command.Description,
                DishPhotoGuid = dishPhotoGuid,
                IngredientsForDish = command.IngredientsForDish.Select(x => new IngredientsForDishDto
                {
                    Id = x.Id ?? 0,
                    IngredientId = x.IngredientId,
                    Quantity = x.Quantity,
                    OkeiCode = x.OkeiCode,
                    Condition = x.Condition
                }).ToList()
            };
            
            return await _dishRepository.EditAsync(dishDto, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Pagination<DishForGridDto>> GetListAsync(DishGetPageQuery query, CancellationToken cancellationToken)
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
        
        /// <summary>
        /// Валидация ингредиентов.
        /// </summary>
        /// <param name="ingredients">Ингредиенты.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <exception cref="ValidationException">Исключение валидации ингредиентов.</exception>
        private async Task ValidateIngredients(IEnumerable<IIngredientsForDish> ingredients, CancellationToken cancellationToken)
        {
            // Валидация передачи ингредиентов для блюда
            if (ingredients == null || !ingredients.Any())
            {
                var ex = new ValidationException($"Не переданы ингредиенты для блюда");
                ex.Data.Add("Ингредиент", $"Не переданы ингредиенты для блюда");

                throw ex;
            }
            
            // Валидация существования ингредиентов
            var ingredientsIds = ingredients.Select(x => x.IngredientId);
            var unknownIngredientsIds = await _ingredientRepository.GetNonExistsIds(ingredientsIds, cancellationToken);
            if (unknownIngredientsIds != null && unknownIngredientsIds.Any())
            {
                var unknownIngredientsIdsInRow = string.Join(", ", unknownIngredientsIds);

                var ex = new ValidationException($"Ингредиенты с идентификаторами [{unknownIngredientsIdsInRow}] не существуют");
                ex.Data.Add("Ингредиент", $"Ингредиенты с идентификаторами [{unknownIngredientsIdsInRow}] не существуют");

                throw ex;
            }
            
            // Валидацию существования Okei-кодов
            var okeisIds = ingredients.Select(x => x.OkeiCode);
            var unknownOkeisIds = await _okeiRepository.GetNonExistsIds(okeisIds, cancellationToken);
            if (unknownOkeisIds != null && unknownOkeisIds.Any())
            {
                var unknownOkeisIdsInRow = string.Join(", ", unknownOkeisIds);

                var ex = new ValidationException($"Okei-коды с кодами [{unknownOkeisIdsInRow}] не существуют");
                ex.Data.Add("Единица измерения", $"Okei-коды с кодами [{unknownOkeisIdsInRow}] не существуют");

                throw ex;
            }
        }
    }
}
