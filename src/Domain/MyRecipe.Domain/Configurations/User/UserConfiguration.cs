using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyRecipe.Domain.Configurations.User;

public class UserConfiguration : IEntityTypeConfiguration<Domain.User.User>
{
    public void Configure(EntityTypeBuilder<Domain.User.User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.States)
            .WithOne()
            .HasForeignKey(x => x.UserId);
    }
}