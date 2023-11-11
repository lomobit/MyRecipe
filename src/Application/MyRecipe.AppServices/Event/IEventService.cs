using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Event;
using MyRecipe.Handlers.Contracts.Event;

namespace MyRecipe.AppServices.Event;

public interface IEventService
{
    /// <summary>
    /// Добавление события в базу данных.
    /// </summary>
    /// <param name="command">Данные о событии.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор добавленного события.</returns>
    Task<int> AddAsync(EventAddCommand command, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод получения списка событий с краткой информацией.
    /// </summary>
    /// <param name="query">Данные запроса.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список событий с краткой информацией.</returns>
    Task<Pagination<EventForGridDto>> GetPageAsync(EventGetPageQuery query, CancellationToken cancellationToken);
}