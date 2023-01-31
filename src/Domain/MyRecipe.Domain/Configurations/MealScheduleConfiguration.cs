using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyRecipe.Domain.Configurations
{
    public class MealScheduleConfiguration : IEntityTypeConfiguration<MealSchedule>
    {
        public void Configure(EntityTypeBuilder<MealSchedule> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Meals)
                .WithOne();
        }
    }
}
