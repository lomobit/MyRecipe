using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyRecipe.Domain.Configurations
{
    public class OkeiConfiguration : IEntityTypeConfiguration<Okei>
    {
        public void Configure(EntityTypeBuilder<Okei> builder)
        {
            builder.HasKey(x => x.Code);
        }
    }
}
