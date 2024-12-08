using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the VacationGolfCourse entity.
    /// </summary>
    public class VacationGolfCourseConfiguration : IEntityTypeConfiguration<VacationGolfCourse>
    {
        public void Configure(EntityTypeBuilder<VacationGolfCourse> builder)
        {
            // Table configuration
            builder.ToTable("VacationGolfCourses");

            // Primary key
            builder.HasKey(vgc => vgc.Id);

            // Property configurations
            builder.Property(vgc => vgc.GolfCourseId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(vgc => vgc.VacationId)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(vgc => vgc.GolfCourse)
                .WithMany(gc => gc.Vacations)
                .HasForeignKey(vgc => vgc.GolfCourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vgc => vgc.Vacation)
                .WithMany(v => v.GolfCourses)
                .HasForeignKey(vgc => vgc.VacationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}