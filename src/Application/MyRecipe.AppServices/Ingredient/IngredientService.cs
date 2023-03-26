
using AutoMapper;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Ingredient;
using MyRecipe.Handlers.Contracts.Ingredient;
using MyRecipe.Infrastructure.Repositories.Ingredient;

namespace MyRecipe.AppServices.Ingredient
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(IngredientDto ingredientDto, CancellationToken cancellationToken)
        {
            return await _ingredientRepository.AddAsync(ingredientDto, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Pagination<IngredientDto>> GetAsync(IngredientGetQuery request, CancellationToken cancellationToken)
        {
            var paginatedResult = await _ingredientRepository.GetAsync(request, cancellationToken);
            return new Pagination<IngredientDto>(
                paginatedResult.Count,
                _mapper.Map<IEnumerable<IngredientDto>>(paginatedResult.ItemsSlice));
        }

        /// <inheritdoc/>
        public async Task<bool> EditAsync(IngredientDto ingredientDto, CancellationToken cancellationToken)
        {
            return await _ingredientRepository.EditAsync(ingredientDto, cancellationToken);
        }
    }
}
