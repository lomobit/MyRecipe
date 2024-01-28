using MyRecipe.Contracts.User;

namespace MyRecipe.AppServices.User;

public interface IUserService
{
    /// <summary>
    /// Получение пользователя.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TokenDto?> GetUserTokenAsync(string email, string password, CancellationToken cancellationToken);
}