using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the Package entity.
    /// </summary>
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            // Table configuration
            builder.ToTable("Packages");

            // Primary key
            builder.HasKey(p => p.Id);

            // Property configurations
            builder.Property(p => p.ShortDescription)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(p => p.LongDescription)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(p => p.Deleted)
                .HasDefaultValue(false);

            builder.Property(p => p.AvailablePartnerUid)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(p => p.SpecialRateId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.LodgingId)
                .HasMaxLength(100)
                .IsRequired(false);

            // Relationships

            // Lodging relationship
            builder.HasOne(p => p.Lodging)
                .WithMany(l => l.AccountTypes) // Assuming Lodging has a collection of Packages
                .HasForeignKey(p => p.LodgingId)
                .OnDelete(DeleteBehavior.SetNull);

            // Rooms relationship
            builder.HasMany(p => p.Rooms)
                .WithOne(r => r.Package)
                .HasForeignKey(r => r.PackageId)
                .OnDelete(DeleteBehavior.Cascade);

            // Bookings relationship
            builder.HasMany(p => p.Bookings)
                .WithOne(b => b.Package)
                .HasForeignKey(b => b.PackageId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(p => p.SpecialRateId)
                .IsUnique();

            builder.HasIndex(p => p.AvailablePartnerUid);
        }
    }
}
