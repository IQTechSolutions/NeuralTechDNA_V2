using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the LodgingSettings entity.
    /// </summary>
    public class LodgingSettingsConfiguration : IEntityTypeConfiguration<LodgingSettings>
    {
        public void Configure(EntityTypeBuilder<LodgingSettings> builder)
        {
            // Table configuration
            builder.ToTable("LodgingSettings");

            // Primary key
            builder.HasKey(ls => ls.Id);

            // Property configurations
            builder.Property(ls => ls.ApiPartner)
                .HasConversion<int>()
                .IsRequired(false);

            builder.Property(ls => ls.UniquePartnerId)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(ls => ls.Active)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(ls => ls.Featured)
                .IsRequired();

            builder.Property(ls => ls.AllowBookings)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(ls => ls.AllowLiveBookings)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(ls => ls.MinAdvanceBookingDays)
                .IsRequired();

            builder.Property(ls => ls.AllowSameDay)
                .IsRequired();

            builder.Property(ls => ls.CutOffTime)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(ls => ls.OneNightStayRefundable)
                .IsRequired();

            builder.Property(ls => ls.ShowCellPhoneNumber)
                .IsRequired();

            builder.Property(ls => ls.AllowSmoking)
                .IsRequired();

            builder.Property(ls => ls.AllowMultipleMealPlans)
                .IsRequired();

            builder.Property(ls => ls.VatRegistered)
                .IsRequired();

            builder.Property(ls => ls.VatNr)
                .HasMaxLength(50)
                .HasDefaultValue(string.Empty)
                .IsRequired(false);

            builder.Property(ls => ls.CheckInTime)
                .HasMaxLength(20)
                .IsRequired()
                .HasDefaultValue("14:00");

            builder.Property(ls => ls.CheckoutTime)
                .HasMaxLength(20)
                .IsRequired()
                .HasDefaultValue("10:00");

            builder.Property(ls => ls.AllowPets)
                .HasMaxLength(100)
                .HasDefaultValue("No pets allowed")
                .IsRequired(false);

            builder.Property(ls => ls.Parking)
                .HasMaxLength(100)
                .HasDefaultValue("Free")
                .IsRequired(false);

            builder.Property(ls => ls.Wifi)
                .HasMaxLength(200)
                .HasDefaultValue("Yes, on entire property")
                .IsRequired(false);

            builder.Property(ls => ls.WifiCost)
                .HasMaxLength(100)
                .HasDefaultValue("Free and unlimited")
                .IsRequired(false);

            // Index configurations
            builder.HasIndex(ls => ls.UniquePartnerId).IsUnique(false);
            builder.HasIndex(ls => ls.ApiPartner);
        }
    }
}
