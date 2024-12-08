using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Configures the Amenity entity.
    /// </summary>
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            // Table configuration
            builder.ToTable("Amenities");

            // Primary key
            builder.HasKey(a => a.Id);

            // Property configurations
            builder.Property(a => a.IconClass)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            // Index configurations
            builder.HasIndex(a => a.Name);
        }
    }
}