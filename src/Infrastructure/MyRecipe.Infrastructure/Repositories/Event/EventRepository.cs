using Microsoft.EntityFrameworkCore;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Enums.Common;
using MyRecipe.Contracts.Event;
using MyRecipe.Handlers.Contracts.Event;

namespace MyRecipe.Infrastructure.Repositories.Event;

public class EventRepository : IEventRepository
{
    private readonly MyRecipeDbContext _context;

    public EventRepository(MyRecipeDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<int> AddAsync(EventAddCommand command, CancellationToken cancellationToken)
    {
        Domain.Event @event;
        try
        {
            @event = new Domain.Event
            {
                Name = command.Name,
                Description = command.Description,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                Meals = command.Meals.Select(x => new Domain.Meal
                {
                    Description = x.Description,
                    Date = x.Date,
                    MealType = x.Type,
                    Dishes = _context.Dishes.Where(dish => x.DishesIds.Contains(dish.Id)).ToArray()
                }).ToArray()
            };

            _context.Events.Add(@event);
            await _context.SaveChangesAsync(cancellationToken);
        }
        finally
        {
            _context.ChangeTracker.Clear();
        }

        return @event.Id;
    }

    /// <inheritdoc />
    public async Task<Pagination<EventForGridDto>> GetPageAsync(EventGetPageQuery query, CancellationToken cancellationToken)
    {
        var commonQuery = _context.Events.AsNoTracking();
        
        // Добавление фильтрации по имени
        if (!string.IsNullOrEmpty(query.NameFilter))
        {
            // TODO: добавить валидацию текстовых полей в Behaviour для всех запросов, потому что сейчас доступна SQL-инъекция для запросов.
            commonQuery = commonQuery.Where(x => x.Name.ToLower().Contains(query.NameFilter.ToLower()));
        }

        var sliceQuery = AddSortingForGetList(commonQuery, query.SortingOrder);
        
        var eventsCount = await commonQuery
            .CountAsync(cancellationToken);
        
        var eventsSlice = await sliceQuery
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(x => new EventForGridDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync(cancellationToken);
        
        return new Pagination<EventForGridDto>(eventsCount, eventsSlice);
    }
    
    /// <summary>
    /// Возвращает IQueryable с добавленной сортировкой.
    /// </summary>
    /// <param name="query">Изначальный IQueryable.</param>
    /// <param name="sortingOrder">Порядок сортировки.</param>
    private IQueryable<Domain.Event> AddSortingForGetList(
        IQueryable<Domain.Event> query,
        SortingOrderEnum sortingOrder)
    {
        switch (sortingOrder)
        {
            case SortingOrderEnum.Descending:
                return query.OrderByDescending(x => x.Id);
            default:
                return query.OrderBy(x => x.Id);
        }
    }
}