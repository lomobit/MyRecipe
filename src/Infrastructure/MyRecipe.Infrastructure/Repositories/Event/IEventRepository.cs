using MyRecipe.Handlers.Contracts.Event;

namespace MyRecipe.Infrastructure.Repositories.Event;

public interface IEventRepository
{
    /// <summary>
    /// Добавление события в базу данных.
    /// </summary>
    /// <param name="command">Данные о событии.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор добавленного события.</returns>
    public Task<int> AddAsync(EventAddCommand command, CancellationToken cancellationToken);
}