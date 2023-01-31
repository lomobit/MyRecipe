
using Microsoft.EntityFrameworkCore;
using MyRecipeLogging.Domain;
using MyRecipeLogging.Domain.Configurations;

namespace MyRecipeLogging.Infrastructure
{
    public class MyRecipeLoggingDbContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

        public MyRecipeLoggingDbContext(DbContextOptions<MyRecipeLoggingDbContext> options) : base(options)
        {
            // TODO: необходимо применять миграции до запуска приложения
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogConfiguration());
        }
    }
}
