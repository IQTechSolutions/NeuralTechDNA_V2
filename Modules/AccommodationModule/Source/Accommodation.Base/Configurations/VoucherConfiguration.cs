using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the Voucher entity.
    /// </summary>
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            // Table configuration
            builder.ToTable("Vouchers");

            // Primary key
            builder.HasKey(v => v.Id);

            // Property configurations
            builder.Property(v => v.Title)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(v => v.ShortDescription)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(v => v.LongDescription)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(v => v.Rate)
                .IsRequired()
                .HasDefaultValue(0.0);

            builder.Property(v => v.MarkupPercentage)
                .IsRequired()
                .HasDefaultValue(0.0);

            builder.Property(v => v.Commission)
                .IsRequired()
                .HasDefaultValue(0.0);

            builder.Property(v => v.Features)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(v => v.Terms)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(v => v.Active)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(v => v.Featured)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(v => v.LodgingId)
                .HasMaxLength(100)
                .IsRequired(false);

            // Relationships
            builder.HasOne(v => v.Lodging)
                .WithMany(l => l.Vouchers)
                .HasForeignKey(v => v.LodgingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Rooms)
                .WithOne(r => r.Voucher)
                .HasForeignKey(r => r.VoucherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Vouchers)
                .WithOne(uv => uv.Voucher)
                .HasForeignKey(uv => uv.VoucherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(v => v.LodgingId);
        }
    }
}


