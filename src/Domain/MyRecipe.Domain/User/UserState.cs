using MyRecipe.Contracts.Enums.User;

namespace MyRecipe.Domain.User;

/// <summary>
/// Состояние записи пользователя.
/// </summary>
public class UserState
{
    /// <summary>
    /// Идентификатор состояния.
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
    /// Версия состояние пользователя.
    /// </summary>
    public uint Version { get; set; }

    /// <summary>
    /// Фамилия. 
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string MiddleName { get; set; }

    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Логин.
    /// </summary>
    public string? Login { get; set; }
    
    /// <summary>
    /// Дата создания записи состояния учетной записи пользователя.
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Роль пользователя в системе.
    /// </summary>
    public RoleEnum Role { get; set; }
}