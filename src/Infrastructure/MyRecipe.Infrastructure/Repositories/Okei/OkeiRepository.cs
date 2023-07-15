using Microsoft.EntityFrameworkCore;

namespace MyRecipe.Infrastructure.Repositories.Okei;

public class OkeiRepository : IOkeiRepository
{
    private readonly MyRecipeDbContext _context;

    public OkeiRepository(MyRecipeDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Domain.Okei>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Okeis.ToArrayAsync(cancellationToken);
    }
}