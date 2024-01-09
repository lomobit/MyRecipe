namespace MyRecipe.Domain.User;

public class UserPassword
{
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