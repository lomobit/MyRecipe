using MyRecipe.Contracts.User;
using MyRecipe.Infrastructure.Repositories.User;

namespace MyRecipe.AppServices.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc/>
    public async Task<UserForSignInDto> GetUserAsync()
    {
        throw new NotImplementedException();
    }
}