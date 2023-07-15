using MediatR;
using MyRecipe.AppServices.Okei;
using MyRecipe.Contracts.Okei;
using MyRecipe.Handlers.Contracts.Okei;

namespace MyRecipe.Handlers.Okei;

public class OkeiGetAllQueryHandler : IRequestHandler<OkeiGetAllQuery, IEnumerable<OkeiDto>>
{
    private readonly IOkeiService _okeiService;

    public OkeiGetAllQueryHandler(IOkeiService okeiService)
    {
        _okeiService = okeiService;
    }

    public async Task<IEnumerable<OkeiDto>> Handle(OkeiGetAllQuery request, CancellationToken cancellationToken)
    {
        return await _okeiService.GetAllAsync(cancellationToken);
    }
}