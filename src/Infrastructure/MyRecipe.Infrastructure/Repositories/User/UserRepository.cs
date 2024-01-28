using Microsoft.EntityFrameworkCore;
using MyRecipe.Contracts.Enums.User;
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

    /// <inheritdoc/>
    public async Task<bool> RegisterNewUser(UserForSignUpDto newUser, CancellationToken cancellationToken)
    {
        try
        {
            var user = new Domain.User.User
            {
                Version = 1,
                CreateTime = DateTime.UtcNow,
                Password = new UserPassword
                {
                    PasswordHash = newUser.PasswordHash,
                    PasswordHashSalt = newUser.PasswordHashSalt,
                    LastUpdateTime = DateTime.UtcNow
                },
                States = new List<UserState>()
            };

            user.States.Add(new UserState
            {
                Version = 1,
                LastName = newUser.LastName,
                FistName = newUser.FirstName,
                MiddleName = newUser.MiddleName,
                Email = newUser.Email,
                CreateTime = DateTime.UtcNow,
                Role = RoleEnum.Visitor
            });

            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);
        }
        finally
        {
            _context.ChangeTracker.Clear();
        }
        
        return true;
    }
}