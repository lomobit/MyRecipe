using MyRecipe.Contracts.User;
using MyRecipe.Handlers.Contracts.User;

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

    /// <summary>
    /// Получение пользователя.
    /// </summary>
    /// <param name="refreshToken">Токен обновления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Токен авторизации.</returns>
    Task<TokenDto?> GetUserTokenWithRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);

    /// <summary>
    /// Регистрация нового пользователя в системе.
    /// </summary>
    /// <param name="command">Данный пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Успешность операции.</returns>
    public Task<bool> UserRegistration(SignUpCommand command, CancellationToken cancellationToken);

    /// <summary>
    /// Изменение профиля для участника мероприятий.
    /// </summary>
    /// <param name="command">Данные для обновления информации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Успешность операции.</returns>
    public Task<bool> EditVisitorProfile(EditVisitorProfileCommand command, CancellationToken cancellationToken);
}