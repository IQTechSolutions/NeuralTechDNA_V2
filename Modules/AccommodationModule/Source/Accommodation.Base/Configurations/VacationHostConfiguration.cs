using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the VacationHost entity.
    /// </summary>
    public class VacationHostConfiguration : IEntityTypeConfiguration<VacationHost>
    {
        public void Configure(EntityTypeBuilder<VacationHost> builder)
        {
            // Table configuration
            builder.ToTable("VacationHosts");

            // Primary key
            builder.HasKey(vh => vh.Id);

            // Property configurations
            builder.Property(vh => vh.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(vh => vh.Description)
                .HasMaxLength(5000);

            // Relationships
            builder.HasMany(vh => vh.Vacations)
                .WithOne(v => v.VacationHost)
                .HasForeignKey(v => v.VacationHostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(vh => vh.Images)
                .WithOne(img => img.Entity)
                .HasForeignKey(img => img.EntityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}