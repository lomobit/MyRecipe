using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyRecipe.Domain.Configurations
{
    public class MealScheduleConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Meals)
                .WithOne();
        }
    }
}
