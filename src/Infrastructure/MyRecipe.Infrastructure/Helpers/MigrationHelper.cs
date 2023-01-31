
namespace MyRecipe.Infrastructure.Helpers
{
    public static class MigrationHelper
    {
        public static string GetSqlForMigrationFromFile(string fileName)
        {
            var sqlFileName = Path.Combine("SqlScriptsForMigrations", fileName);
            var filePath = Path.Combine(GetBasePath(), sqlFileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            return File.ReadAllText(filePath);
        }

        private static string GetBasePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
