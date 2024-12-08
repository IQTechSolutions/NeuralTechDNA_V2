using Accommodation.Base.Entities.Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the GolfCourse entity.
    /// </summary>
    public class GolfCourseConfiguration : IEntityTypeConfiguration<GolfCourse>
    {
        public void Configure(EntityTypeBuilder<GolfCourse> builder)
        {
            // Table configuration
            builder.ToTable("GolfCourses");

            // Primary key
            builder.HasKey(gc => gc.Id);

            // Property configurations
            builder.Property(gc => gc.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(gc => gc.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(gc => gc.Location)
                .HasMaxLength(1000)
                .IsRequired(false);

            // Relationships
            builder.HasMany(gc => gc.Vacations)
                .WithOne(vgc => vgc.GolfCourse)
                .HasForeignKey(vgc => vgc.GolfCourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(gc => gc.Name);
        }
    }
}