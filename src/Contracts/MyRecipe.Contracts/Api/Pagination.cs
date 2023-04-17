namespace MyRecipe.Contracts.Api
{
    /// <summary>
    /// Класс пагинации элементов.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pagination<T>
    {
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
        public Pagination(int count, IEnumerable<T> itemsSlice)
        {
            Count = count;
            ItemsSlice = itemsSlice;
        }
    }
}
