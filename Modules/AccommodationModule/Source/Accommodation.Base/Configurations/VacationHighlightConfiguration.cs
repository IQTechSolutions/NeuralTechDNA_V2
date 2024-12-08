using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the VacationHighlight entity.
    /// </summary>
    public class VacationHighlightConfiguration : IEntityTypeConfiguration<VacationHighlight>
    {
        public void Configure(EntityTypeBuilder<VacationHighlight> builder)
        {
            // Table configuration
            builder.ToTable("VacationHighlights");

            // Primary key
            builder.HasKey(vh => vh.Id);

            // Property configurations
            builder.Property(vh => vh.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(vh => vh.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(vh => vh.VacationId)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(vh => vh.Vacation)
                .WithMany(v => v.VacationHighlights)
                .HasForeignKey(vh => vh.VacationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}