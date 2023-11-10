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
    public Task<int> AddAsync(EventAddCommand command, CancellationToken cancellationToken);
}