using Microsoft.EntityFrameworkCore;
using MyRecipe.Domain;
using MyRecipe.Domain.Configurations;
using MyRecipe.Domain.Configurations.User;
using MyRecipe.Domain.User;

namespace MyRecipe.Infrastructure;

public class MyRecipeDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserState> UserStates { get; set; }
    public DbSet<UserPassword> UserPasswords { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Okei> Okeis { get; set; }
    public DbSet<IngredientsForDish> IngredientsForDishes { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<Event> Events { get; set; }


    public MyRecipeDbContext(DbContextOptions<MyRecipeDbContext> options) : base(options)
    {
        // TODO: необходимо применять миграции до запуска приложения
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserStateConfiguration());
        modelBuilder.ApplyConfiguration(new UserPasswordConfiguration());
        modelBuilder.ApplyConfiguration(new UserRefreshTokenConfiguration());
        
        modelBuilder.ApplyConfiguration(new IngredientConfiguration());
        modelBuilder.ApplyConfiguration(new DishConfiguration());
        modelBuilder.ApplyConfiguration(new OkeiConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientsForDishConfiguration());
        modelBuilder.ApplyConfiguration(new MealConfiguration());
        modelBuilder.ApplyConfiguration(new MealScheduleConfiguration());
    }
}