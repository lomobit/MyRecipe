using MyRecipe.Contracts.Okei;

namespace MyRecipe.AppServices.Okei;

public interface IOkeiService
{
    /// <summary>
    /// Получение из базы списка всех Okei-кодов.
    /// </summary>
    /// <param name="query">Данные запроса.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список ингредиентов.</returns>
    Task<IEnumerable<OkeiDto>> GetAllAsync(CancellationToken cancellationToken);
}