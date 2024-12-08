using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the VacationReference entity.
    /// </summary>
    public class VacationReferenceConfiguration : IEntityTypeConfiguration<VacationReference>
    {
        public void Configure(EntityTypeBuilder<VacationReference> builder)
        {
            // Table name
            builder.ToTable("VacationReferences");

            // Primary key
            builder.HasKey(vr => vr.Id);

            // Properties
            builder.Property(vr => vr.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(vr => vr.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(vr => vr.VacationId)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(vr => vr.Vacation)
                .WithMany(v => v.References)
                .HasForeignKey(vr => vr.VacationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
