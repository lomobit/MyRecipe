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

    /// <inheritdoc/>
    public async Task<string[]> GetNonExistsIds(IEnumerable<string> codes, CancellationToken cancellationToken)
    {
        var existsIds = await _context.Okeis
            .AsNoTracking()
            .Where(i => codes.Contains(i.Code))
            .Select(i => i.Code)
            .ToArrayAsync(cancellationToken);

        return codes.Except(existsIds).ToArray();
    }
}