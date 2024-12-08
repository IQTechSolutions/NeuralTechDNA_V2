using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the Order entity.
    /// </summary>
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Table configuration
            builder.ToTable("Orders");

            // Primary key
            builder.HasKey(o => o.Id);

            // Property configurations
            builder.Property(o => o.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.PhoneNr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.SubTotalExcl)
                .IsRequired()
                .HasColumnType("double");

            builder.Property(o => o.Vat)
                .IsRequired()
                .HasColumnType("double");

            builder.Property(o => o.SubTotalIncl)
                .IsRequired()
                .HasColumnType("double");

            builder.Property(o => o.Discount)
                .IsRequired()
                .HasColumnType("double");

            builder.Property(o => o.TotalDue)
                .IsRequired()
                .HasColumnType("double");

            // Relationships

            // Bookings relationship
            builder.HasMany(o => o.Bookings)
                .WithOne(b => b.Order)
                .HasForeignKey(b => b.OrderNr)
                .OnDelete(DeleteBehavior.Cascade);

            // Vouchers relationship
            builder.HasMany(o => o.Vouchers)
                .WithOne(v => v.Order)
                .HasForeignKey(v => v.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(o => o.Email).IsUnique(false);
            builder.HasIndex(o => o.PhoneNr).IsUnique(false);
        }
    }
}
