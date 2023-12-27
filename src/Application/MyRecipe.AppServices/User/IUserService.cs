using MyRecipe.Contracts.User;

namespace MyRecipe.AppServices.User;

public interface IUserService
{
    /// <summary>
    /// Получение пользователя.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="host"></param>
    /// <returns></returns>
    Task<string?> GetUserTokenAsync(string email, string password);
}