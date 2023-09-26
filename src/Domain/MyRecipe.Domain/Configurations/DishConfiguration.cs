using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyRecipe.Domain.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.IngredientsForDish)
                .WithOne()
                .HasForeignKey(x => x.DishId);
        }
    }
}
