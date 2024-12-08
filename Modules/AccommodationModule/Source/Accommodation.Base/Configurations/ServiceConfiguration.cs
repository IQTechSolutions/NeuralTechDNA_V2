using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the Service entity.
    /// </summary>
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            // Table configuration
            builder.ToTable("Services");

            // Primary key
            builder.HasKey(s => s.Id);

            // Property configurations
            builder.Property(s => s.Location)
                .HasMaxLength(200);

            builder.Property(s => s.ServiceName)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(s => s.DisplayName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Supplier)
                .HasMaxLength(100);

            builder.Property(s => s.SupplierName)
                .HasMaxLength(200);

            builder.Property(s => s.Code)
                .HasMaxLength(50);

            builder.Property(s => s.UniqueId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.CommPerc)
                .HasColumnType("decimal(5,2)");

            builder.Property(s => s.MarkupPerc)
                .HasColumnType("decimal(5,2)");

            builder.Property(s => s.CurrentRate)
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.RoomsAvailable)
                .HasMaxLength(100);

            builder.Property(s => s.RoomRateTypeDescription)
                .HasMaxLength(500);

            builder.Property(s => s.RateCode)
                .HasMaxLength(50);

            builder.Property(s => s.Includes)
                .HasMaxLength(1000);

            builder.Property(s => s.Excludes)
                .HasMaxLength(1000);

            builder.Property(s => s.RoomInformation)
                .HasMaxLength(1000);

            builder.Property(s => s.ChildPolicy)
                .HasMaxLength(1000);

            builder.Property(s => s.CancellationPolicy)
                .HasMaxLength(1000);

            builder.Property(s => s.BookingTerms)
                .HasMaxLength(1000);

            // Relationships

            // Package relationship
            builder.HasOne(s => s.Package)
                .WithMany()
                .HasForeignKey(s => s.PackageId)
                .OnDelete(DeleteBehavior.Restrict);

            // Lodging relationship
            builder.HasOne(s => s.Lodging)
                .WithMany(l => l.Services)
                .HasForeignKey(s => s.LodgingId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            // AvailablePartner relationship
            builder.HasOne(s => s.AvailablePartner)
                .WithMany(ap => ap.Services)
                .HasForeignKey(s => s.AvailablePartnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ChildAgeParams relationship
            builder.HasMany(s => s.ChildAgeParams)
                .WithOne(cap => cap.Service)
                .HasForeignKey(cap => cap.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Rates relationship
            builder.HasMany(s => s.Rates)
                .WithOne(r => r.Service)
                .HasForeignKey(r => r.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Additional configurations if needed
            // Example: Configuring indexes
            builder.HasIndex(s => s.UniqueId)
                .IsUnique();
        }
    }
}
