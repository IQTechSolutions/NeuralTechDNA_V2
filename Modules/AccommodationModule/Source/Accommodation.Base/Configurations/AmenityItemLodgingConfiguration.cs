using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the AmenityItem<Lodging,string> entity using EF Core fluent API.
    /// </summary>
    public class AmenityItemLodgingConfiguration : IEntityTypeConfiguration<AmenityItem<Lodging, string>>
    {
        public void Configure(EntityTypeBuilder<AmenityItem<Lodging, string>> builder)
        {
            // Table Name
            builder.ToTable("AmenityItems");

            // Primary Key
            builder.HasKey(ai => ai.Id);

            // Foreign Keys & Navigation Properties
            builder.HasOne(ai => ai.Amenity)
                .WithMany() // or WithMany(navigationName) if defined
                .HasForeignKey(ai => ai.AmenityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ai => ai.Lodging)
                .WithMany(l => l.Amenities)
                .HasForeignKey(ai => ai.LodgingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ai => ai.Room)
                .WithMany() // If Room has a collection of AmenityItems, specify it here
                .HasForeignKey(ai => ai.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}