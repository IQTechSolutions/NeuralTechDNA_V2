using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the VacationLodging entity.
    /// </summary>
    public class VacationLodgingConfiguration : IEntityTypeConfiguration<VacationLodging>
    {
        public void Configure(EntityTypeBuilder<VacationLodging> builder)
        {
            // Table configuration
            builder.ToTable("VacationLodgings");

            // Primary key
            builder.HasKey(vl => vl.Id);

            // Property configurations
            builder.Property(vl => vl.LodgingId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(vl => vl.VacationId)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(vl => vl.Lodging)
                .WithMany(l => l.Vacations) // Ensure Lodging has a collection property for VacationLodgings
                .HasForeignKey(vl => vl.LodgingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vl => vl.Vacation)
                .WithMany(v => v.Lodgings) // Ensure Vacation has a collection property for VacationLodgings
                .HasForeignKey(vl => vl.VacationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}