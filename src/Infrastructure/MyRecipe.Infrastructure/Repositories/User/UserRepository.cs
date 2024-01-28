using Microsoft.EntityFrameworkCore;
using MyRecipe.Contracts.User;
using MyRecipe.Domain.User;

namespace MyRecipe.Infrastructure.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly MyRecipeDbContext _context;

    public UserRepository(MyRecipeDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<UserPasswordDto?> GetUserIdWithPassword(string email)
    {
        return await _context.Users
            .Join(_context.UserStates,
                u => new { Id = u.Id, Version = u.Version, Email = email },
                us => new { Id = us.UserId, Version = us.Version, Email = us.Email },
                (u, us) => new UserPasswordDto(u.Id, u.Password.PasswordHash, u.Password.PasswordHashSalt))
            .FirstOrDefaultAsync();
    }

    /// <inheritdoc/>
    public async Task<UserForSignInDto> GetUserForSignInAsync(Guid userId)
    {
        return await _context.Users
            .Join(_context.UserStates,
                u => new { Id = u.Id, Version = u.Version },
                us => new { Id = us.UserId, Version = us.Version },
                (u, us) => new UserForSignInDto(
                    us.FistName,
                    us.MiddleName,
                    us.LastName,
                    us.Role
                ))
            .FirstAsync();
    }

    /// <inheritdoc/>
    public async Task AddUserRefreshTokenAsync(Guid userId, RefreshTokenDto refreshToken, CancellationToken cancellationToken)
    {
        try
        {
            _context.UserRefreshTokens.Add(new UserRefreshToken
            {
                UserId = userId,
                RefreshToken = refreshToken.Token,
                ExpirationTime = refreshToken.ExpirationTime
            });
            await _context.SaveChangesAsync(cancellationToken);
        }
        finally
        {
            _context.ChangeTracker.Clear();
        }
    }
}