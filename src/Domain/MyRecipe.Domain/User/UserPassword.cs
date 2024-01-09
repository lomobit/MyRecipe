namespace MyRecipe.Domain.User;

public class UserPassword
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
    /// Хеш пользовательского пароля.
    /// </summary>
    public string PasswordHash { get; set; }
    
    /// <summary>
    /// Соль, используемая для хеширования текущего пароля.
    /// </summary>
    public string PasswordHashSalt { get; set; }
    
    /// <summary>
    /// Дата последнего обновление пароля.
    /// </summary>
    public DateTime LastUpdateTime { get; set; }
}