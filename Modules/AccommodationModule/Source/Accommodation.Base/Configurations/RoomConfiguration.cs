using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the Room entity.
    /// </summary>
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            // Table configuration
            builder.ToTable("Rooms");

            // Primary key
            builder.HasKey(r => r.Id);

            // Property configurations
            builder.Property(r => r.PartnerRoomTypeId)
                .IsRequired(false);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.Description)
                .HasMaxLength(1000);

            builder.Property(r => r.AdditionalInfo)
                .HasMaxLength(1000);

            builder.Property(r => r.BedCount)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(r => r.RoomCount)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(r => r.MaxOccupancy)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(r => r.MaxAdults)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(r => r.BookingTerms)
                .HasMaxLength(1000);

            builder.Property(r => r.CancellationPolicy)
                .HasMaxLength(1000);

            builder.Property(r => r.DefaultBedTypeId)
                .HasMaxLength(100);

            builder.Property(r => r.DefaultMealPlanId)
                .HasMaxLength(100);

            builder.Property(r => r.Commision)
                .IsRequired()
                .HasDefaultValue(4)
                .HasColumnType("double");

            builder.Property(r => r.MarkUp)
                .IsRequired()
                .HasDefaultValue(20)
                .HasColumnType("double");

            builder.Property(r => r.SpecialRate)
                .IsRequired()
                .HasColumnType("double");

            builder.Property(r => r.VoucherRate)
                .IsRequired()
                .HasColumnType("double");

            builder.Property(r => r.RateScheme)
                .HasConversion<int>()
                .IsRequired(false);

            // Relationships

            // Package relationship
            builder.HasOne(r => r.Package)
                .WithMany(p => p.Rooms)
                .HasForeignKey(r => r.PackageId)
                .OnDelete(DeleteBehavior.SetNull);

            // Voucher relationship
            builder.HasOne(r => r.Voucher)
                .WithMany(v => v.Rooms)
                .HasForeignKey(r => r.VoucherId)
                .OnDelete(DeleteBehavior.SetNull);

            // MealPlans relationship
            builder.HasMany(r => r.MealPlans)
                .WithOne()
                .HasForeignKey(mp => mp.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // BedTypes relationship
            builder.HasMany(r => r.BedTypes)
                .WithOne(bt => bt.Room)
                .HasForeignKey(bt => bt.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // ServiceAmenities relationship
            builder.HasMany(r => r.Amneties)
                .WithOne()
                .HasForeignKey(sa => sa.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // FeaturedImages relationship
            builder.HasMany(r => r.FeaturedImages)
                .WithOne()
                .HasForeignKey(fi => fi.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // ChildPolicyRules relationship
            builder.HasMany(r => r.ChildPolicyRules)
                .WithOne(cpr => cpr.Room)
                .HasForeignKey(cpr => cpr.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(r => r.Name).IsUnique(false);
            builder.HasIndex(r => r.PackageId);
            builder.HasIndex(r => r.VoucherId);
        }
    }
}
