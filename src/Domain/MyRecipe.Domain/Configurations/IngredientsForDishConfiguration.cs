using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyRecipe.Domain.Configurations
{
    public class IngredientsForDishConfiguration : IEntityTypeConfiguration<IngredientsForDish>
    {
        public void Configure(EntityTypeBuilder<IngredientsForDish> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Dish)
                .WithMany(x => x.IngredientsForDish)
                .HasForeignKey(x => x.DishId);
            builder.HasOne(x => x.Ingredient)
                .WithMany()
                .HasForeignKey(x => x.IngredientId);
            builder.HasOne(x => x.Okei)
                .WithMany()
                .HasForeignKey(x => x.OkeiCode);
        }
    }
}
