namespace MyRecipe.Contracts.Enums.User;

/// <summary>
/// Роли пользователей системы.
/// </summary>
public enum RoleEnum
{
    /// <summary>
    /// Участник мероприятия.
    /// </summary>
    Visitor = 1,
    
    /// <summary>
    /// Организатор мероприятия
    /// </summary>
    Organizer = 2,
    
    /// <summary>
    /// Администратор площадки
    /// </summary>
    Administrator = 3
}