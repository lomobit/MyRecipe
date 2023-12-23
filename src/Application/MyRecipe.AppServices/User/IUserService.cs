using MyRecipe.Contracts.User;

namespace MyRecipe.AppServices.User;

public interface IUserService
{
    /// <summary>
    /// Получение пользователя.
    /// </summary>
    /// <returns></returns>
    Task<UserForSignInDto> GetUserAsync();
}