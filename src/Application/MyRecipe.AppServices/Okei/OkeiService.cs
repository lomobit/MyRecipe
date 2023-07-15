using AutoMapper;
using MyRecipe.Contracts.Okei;
using MyRecipe.Infrastructure.Repositories.Okei;

namespace MyRecipe.AppServices.Okei;

public class OkeiService : IOkeiService
{
    private readonly IOkeiRepository _okeiRepository;
    private readonly IMapper _mapper;

    public OkeiService(IOkeiRepository okeiRepository, IMapper mapper)
    {
        _okeiRepository = okeiRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<OkeiDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<OkeiDto>>(await _okeiRepository.GetAllAsync(cancellationToken));
    }
}