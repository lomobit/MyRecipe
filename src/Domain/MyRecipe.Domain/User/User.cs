namespace MyRecipe.Domain.User;

/// <summary>
/// Сущность пользователя 
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Актуальная версия состояния пользователя.
    /// </summary>
    public uint Version { get; set; }

    /// <summary>
    /// Дата создания учетной записи.
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Сущность пароля для пользователя.
    /// </summary>
    public virtual UserPassword Password { get; set; }

    /// <summary>
    /// Токены для обновления JWT-токенов пользователя.
    /// </summary>
    public virtual List<UserRefreshToken> RefreshTokens { get; set; }

    /// <summary>
    /// Состояния учетной записи пользователя.
    /// </summary>
    public virtual List<UserState> States { get; set; }
}