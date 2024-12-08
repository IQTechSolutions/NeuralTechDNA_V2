using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the Vacation entity.
    /// </summary>
    public class VacationConfiguration : IEntityTypeConfiguration<Vacation>
    {
        public void Configure(EntityTypeBuilder<Vacation> builder)
        {
            // Table configuration
            builder.ToTable("Vacations");

            // Primary key
            builder.HasKey(v => v.Id);

            // Property configurations
            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(v => v.Description)
                .HasMaxLength(5000);

            builder.Property(v => v.GeneralInformation)
                .HasMaxLength(5000);

            builder.Property(v => v.StartDate)
                .IsRequired();

            builder.Property(v => v.EndDate)
                .IsRequired();

            builder.Property(v => v.RoomCount)
                .IsRequired();

            builder.Property(v => v.MaxBookingCount)
                .IsRequired();

            builder.Property(v => v.BookingProcess)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(v => v.PriceExclusions)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(v => v.PaymentTerms)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(v => v.VacationHostId)
                .HasMaxLength(100);

            // Relationships

            // VacationHost relationship
            builder.HasOne(v => v.VacationHost)
                .WithMany(h => h.Vacations)
                .HasForeignKey(v => v.VacationHostId)
                .OnDelete(DeleteBehavior.SetNull);

            // Prices relationship
            builder.HasMany(v => v.Prices)
                .WithOne(p => p.Vacation)
                .HasForeignKey(p => p.VacationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Inclusions relationship
            builder.HasMany(v => v.Inclusions)
                .WithOne(i => i.Vacation)
                .HasForeignKey(i => i.VacationId)
                .OnDelete(DeleteBehavior.Cascade);

            // References relationship
            builder.HasMany(v => v.References)
                .WithOne(r => r.Vacation)
                .HasForeignKey(r => r.VacationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Itineraries relationship
            builder.HasMany(v => v.Itineraries)
                .WithOne(i => i.Vacation)
                .HasForeignKey(i => i.VacationId)
                .OnDelete(DeleteBehavior.Cascade);

            // VacationHighlights relationship
            builder.HasMany(v => v.VacationHighlights)
                .WithOne(h => h.Vacation)
                .HasForeignKey(h => h.VacationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Destinations relationship
            builder.HasMany(v => v.Destinations)
                .WithOne(d => d.Vacation)
                .HasForeignKey(d => d.VacationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Lodgings relationship
            builder.HasMany(v => v.Lodgings)
                .WithOne(l => l.Vacation)
                .HasForeignKey(l => l.VacationId)
                .OnDelete(DeleteBehavior.Cascade);

            // GolfCourses relationship
            builder.HasMany(v => v.GolfCourses)
                .WithOne(gc => gc.Vacation)
                .HasForeignKey(gc => gc.VacationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
