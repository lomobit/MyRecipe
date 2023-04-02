namespace MyRecipe.Contracts.File
{
    public class FileDto
    {
        /// <summary>
        /// Guid файла.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Наименование файла.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Содержимое файла.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public byte[] Content { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Размер файла в байтах.
        /// </summary>
        public long Size { get; set; }
    }
}
