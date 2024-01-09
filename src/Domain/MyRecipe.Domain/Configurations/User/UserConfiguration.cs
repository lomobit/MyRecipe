using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRecipe.Domain.User;

namespace MyRecipe.Domain.Configurations.User;

public class UserConfiguration : IEntityTypeConfiguration<Domain.User.User>
{
    public void Configure(EntityTypeBuilder<Domain.User.User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.States)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.RefreshTokens)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Password)
            .WithOne()
            .HasForeignKey<UserPassword>(s => s.UserId);
    }
}