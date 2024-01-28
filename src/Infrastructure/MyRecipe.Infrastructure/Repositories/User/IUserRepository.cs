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

    /// <summary>
    /// Добавляет новый токен обновления для пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="refreshToken">Токен обновления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task AddUserRefreshTokenAsync(Guid userId, RefreshTokenDto refreshToken, CancellationToken cancellationToken);

    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="newUser">Данные о новом пользователе</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Успешность операции.</returns>
    public Task<bool> RegisterNewUser(UserForSignUpDto newUser, CancellationToken cancellationToken);
}