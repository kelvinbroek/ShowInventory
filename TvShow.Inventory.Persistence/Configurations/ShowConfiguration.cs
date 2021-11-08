using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TvShow.Inventory.Domain.Entities;

namespace TvShow.Inventory.Persistence.Configurations
{
    public class ShowConfiguration : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.ToTable("Shows");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(128).IsRequired();
            builder.Property(b => b.Language).HasConversion<string>();
        }
    }
}
