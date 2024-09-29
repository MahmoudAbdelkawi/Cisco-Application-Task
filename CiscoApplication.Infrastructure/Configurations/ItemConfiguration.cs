using CiscoApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CiscoApplication.Infrastructure.Configurations
{
    internal class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Band)
                .IsRequired();

            builder.Property(x => x.CategoryCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Manufacturer)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.PartSKU)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.PartSKU)
                .IsUnique();

            builder.Property(x => x.ItemDescription)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ListPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.MinimumDiscount)
                .IsRequired()
                .HasColumnType("decimal(3,2)");
        }
    }
}
