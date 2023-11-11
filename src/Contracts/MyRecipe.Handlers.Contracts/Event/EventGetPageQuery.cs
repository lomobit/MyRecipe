using MediatR;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Enums.Common;
using MyRecipe.Contracts.Event;

namespace MyRecipe.Handlers.Contracts.Event;

public class EventGetPageQuery: IRequest<Pagination<EventForGridDto>>
{
    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Размер страницы.
    /// </summary>
    public int PageSize { get; set; } = 15;
    
    /// <summary>
    /// Порядок сортировки.
    /// </summary>
    public SortingOrderEnum SortingOrder { get; set; } = SortingOrderEnum.Ascending;
    
    /// <summary>
    /// Фильтр по названию события.
    /// </summary>
    public string? NameFilter { get; set; }
}