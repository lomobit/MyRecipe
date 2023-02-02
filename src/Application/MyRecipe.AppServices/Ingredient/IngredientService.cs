
using AutoMapper;
using MyRecipe.Contracts.Ingredient;
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
        public async Task<IEnumerable<IngredientDto>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<IngredientDto>>(await _ingredientRepository.GetAsync(pageNumber, pageSize, cancellationToken));
        }
    }
}
