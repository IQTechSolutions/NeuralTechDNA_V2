using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the VacationDestination entity.
    /// </summary>
    public class VacationDestinationConfiguration : IEntityTypeConfiguration<VacationDestination>
    {
        public void Configure(EntityTypeBuilder<VacationDestination> builder)
        {
            // Table configuration
            builder.ToTable("VacationDestinations");

            // Primary key
            builder.HasKey(vd => vd.Id);

            // Property configurations
            builder.Property(vd => vd.VacationId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(vd => vd.DestinationId)
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(vd => vd.Vacation)
                .WithMany(v => v.Destinations)
                .HasForeignKey(vd => vd.VacationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vd => vd.Destination)
                .WithMany(d => d.Vacations)
                .HasForeignKey(vd => vd.DestinationId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}