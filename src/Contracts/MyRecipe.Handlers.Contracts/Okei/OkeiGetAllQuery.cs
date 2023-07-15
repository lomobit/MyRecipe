using MediatR;
using MyRecipe.Contracts.Okei;

namespace MyRecipe.Handlers.Contracts.Okei;

public class OkeiGetAllQuery : IRequest<IEnumerable<OkeiDto>>
{
    
}