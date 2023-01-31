
namespace MyRecipe.Domain
{
    /// <summary>
    /// Общероссийский классификатор единиц измерения
    /// </summary>
    public class Okei
    {
        /// <summary>
        /// Код ОКЕИ (единицы измерения)
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Code { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Наименование единицы измерения
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Условное обозначение (национальное)
        /// </summary>
        public string? ConventionDesignationNational { get; set; }

        /// <summary>
        /// Условное обозначение (международное)
        /// </summary>
        public string? ConventionDesignationInternational { get; set; }

        /// <summary>
        /// Кодовое обозначение (национальное)
        /// </summary>
        public string? CodeDesignationNational { get; set; }

        /// <summary>
        /// Кодовое обозначение (международное)
        /// </summary>
        public string? CodeDesignationInternational { get; set; }

    }
}
