using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TvShow.Inventory.Domain.Entities;

namespace TvShow.Inventory.Persistence.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(128).IsRequired();
        }
    }
}
