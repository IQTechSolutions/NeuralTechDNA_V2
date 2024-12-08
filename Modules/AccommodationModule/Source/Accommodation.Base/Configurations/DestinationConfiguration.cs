using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the Destination entity.
    /// </summary>
    public class DestinationConfiguration : IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> builder)
        {
            // Table configuration
            builder.ToTable("Destinations");

            // Primary key
            builder.HasKey(d => d.Id);

            // Property configurations
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(5000);

            // Relationships
            builder.HasMany(d => d.Vacations)
                .WithOne(vd => vd.Destination)
                .HasForeignKey(vd => vd.DestinationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(d => d.Name);
        }
    }
}