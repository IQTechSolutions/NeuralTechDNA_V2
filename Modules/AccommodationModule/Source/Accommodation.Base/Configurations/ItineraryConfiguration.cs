using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the Itinerary entity.
    /// </summary>
    public class ItineraryConfiguration : IEntityTypeConfiguration<Itinerary>
    {
        public void Configure(EntityTypeBuilder<Itinerary> builder)
        {
            // Table configuration
            builder.ToTable("Itineraries");

            // Primary key
            builder.HasKey(i => i.Id);

            // Property configurations
            builder.Property(i => i.Date)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(i => i.VacationId)
                .HasMaxLength(100)
                .IsRequired(false);

            // Relationships
            builder.HasOne(i => i.Vacation)
                .WithMany(v => v.Itineraries)
                .HasForeignKey(i => i.VacationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.ItineraryDetails)
                .WithOne(ii => ii.Itinerary)
                .HasForeignKey(ii => ii.ItineraryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(i => i.VacationId);
        }
    }
}