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
}