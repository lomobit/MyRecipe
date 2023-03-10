
namespace MyRecipe.Contracts.Api
{
    /// <summary>
    /// Класс пагинации элементов.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pagination<T>
    {
        /// <summary>
        /// Количество элементов на странице.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Номер страницы.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Количество всех элементов.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Срез элементов для данной страницы.
        /// </summary>
        public IEnumerable<T> ItemsSlice { get; set; }

        /// <summary>
        /// Конструктор <see cref="Pagination"/>
        /// </summary>
        /// <param name="itemsSlice"></param>
        public Pagination(int pageSize, int pageNumber, int count, IEnumerable<T> itemsSlice)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            Count = count;
            ItemsSlice = itemsSlice;
        }
    }
}
