using MyRecipe.Contracts.User;

namespace MyRecipe.Infrastructure.Repositories.User;

public interface IUserRepository
{
    /// <summary>
    /// Получение данных о пароля пользователя по Email.
    /// </summary>
    /// <param name="email">Email.</param>
    public Task<UserPasswordDto?> GetUserIdWithPassword(string email);
    
    /// <summary>
    /// Получить пользователя по идентификатору.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <exception cref="System.InvalidOperationException">В случае, если пользователя с указанным идентификатором не удалось найти.</exception>
    public Task<UserForSignInDto> GetUserForSignInAsync(Guid userId);
}