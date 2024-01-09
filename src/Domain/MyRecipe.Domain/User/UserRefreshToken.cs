namespace MyRecipe.Domain.User;

public class UserRefreshToken
{
    /// <summary>
    /// Идентификатор записи.
    /// </summary>
    public uint Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Сущность пользователя.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// Токен для обновления JWT-токена.
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    /// Дата истечения срока токена.
    /// </summary>
    public DateTime ExpirationTime { get; set; }
}