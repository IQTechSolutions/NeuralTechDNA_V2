using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the ItineraryItem entity.
    /// </summary>
    public class ItineraryItemConfiguration : IEntityTypeConfiguration<ItineraryItem>
    {
        public void Configure(EntityTypeBuilder<ItineraryItem> builder)
        {
            // Table configuration
            builder.ToTable("ItineraryItems");

            // Primary key
            builder.HasKey(ii => ii.Id);

            // Property configurations
            builder.Property(ii => ii.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(ii => ii.ItineraryId)
                .HasMaxLength(100)
                .IsRequired(false);

            // Relationships
            builder.HasOne(ii => ii.Itinerary)
                .WithMany(i => i.ItineraryDetails)
                .HasForeignKey(ii => ii.ItineraryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(ii => ii.ItineraryId);
        }
    }
}