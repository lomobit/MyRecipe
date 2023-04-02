using Microsoft.EntityFrameworkCore;
using MyRecipeFiles.Domain.Configurations;

namespace MyRecipeFiles.Infrastructure
{
    public class MyRecipeFilesDbContext : DbContext
    {
        public DbSet<Domain.File> Files { get; set; }

        public MyRecipeFilesDbContext(DbContextOptions<MyRecipeFilesDbContext> options) : base(options)
        {
            // TODO: необходимо применять миграции до запуска приложения
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FileConfiguration());
        }
    }
}
