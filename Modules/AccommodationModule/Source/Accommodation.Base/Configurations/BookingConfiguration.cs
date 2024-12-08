using Accommodation.Base.Entities;
using Accommodation.Base.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the Booking entity.
    /// </summary>
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // Table configuration
            builder.ToTable("Bookings");

            // Primary key
            builder.HasKey(b => b.Id);

            // Property configurations
            builder.Property(b => b.BookingReferenceNr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(b => b.Contacts)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(b => b.PhoneNr)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(b => b.Email)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(b => b.Website)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(b => b.Adress)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(b => b.Lat)
                .IsRequired(false);

            builder.Property(b => b.Lng)
                .IsRequired(false);

            builder.Property(b => b.Directions)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(b => b.PaymentInstructions)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(b => b.StartDate)
                .IsRequired();

            builder.Property(b => b.EndDate)
                .IsRequired();

            builder.Property(b => b.RoomQty)
                .IsRequired();

            builder.Property(b => b.RateId)
                .IsRequired();

            builder.Property(b => b.RateDescription)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(b => b.Adults)
                .IsRequired();

            builder.Property(b => b.Children)
                .IsRequired();

            builder.Property(b => b.Infants)
                .IsRequired();

            builder.Property(b => b.CancellationId)
                .IsRequired();

            builder.Property(b => b.BookingStatus)
                .IsRequired()
                .HasDefaultValue(BookingStatus.Pending);

            builder.Property(b => b.RoomId)
                .IsRequired(false);

            builder.Property(b => b.PackageId)
                .IsRequired(false);

            builder.Property(b => b.LodgingId)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(b => b.UserId)
                .IsRequired(false);

            builder.Property(b => b.OrderNr)
                .IsRequired(false);

            // Relationships
            builder.HasOne(b => b.Room)
                .WithMany()
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Package)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PackageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Lodging)
                .WithMany()
                .HasForeignKey(b => b.LodgingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Order)
                .WithMany(o => o.Bookings)
                .HasForeignKey(b => b.OrderNr)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(b => b.BookingReferenceNr).IsUnique();
            builder.HasIndex(b => b.RoomId);
            builder.HasIndex(b => b.PackageId);
            builder.HasIndex(b => b.LodgingId);
            builder.HasIndex(b => b.UserId);
            builder.HasIndex(b => b.OrderNr);
        }
    }
}


