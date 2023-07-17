namespace MyRecipe.Infrastructure.Repositories.Okei;

public interface IOkeiRepository
{
    /// <summary>
    /// Получение из базы списка всех Okei-кодов.
    /// </summary>
    /// <param name="query">Данные запроса.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список ингредиентов.</returns>
    Task<IEnumerable<Domain.Okei>> GetAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Проверить какие Okei-коды не существуют.
    /// </summary>
    /// <param name="codes">Идентификаторы ингредиентов.</param>
    /// /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Переданные идентификаторы, которые не существуют.</returns>
    Task<string[]> GetNonExistsIds(IEnumerable<string> codes, CancellationToken cancellationToken);
}