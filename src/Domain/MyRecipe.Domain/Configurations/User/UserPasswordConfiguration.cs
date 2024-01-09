using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRecipe.Domain.User;

namespace MyRecipe.Domain.Configurations.User;

public class UserPasswordConfiguration : IEntityTypeConfiguration<UserPassword>
{
    public void Configure(EntityTypeBuilder<UserPassword> builder)
    {
        builder.HasOne(x => x.User)
            .WithOne(x => x.Password);
    }
}