using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Entities
{
    /// <summary>
    /// Configures the Inclusions entity.
    /// </summary>
    public class InclusionsConfiguration : IEntityTypeConfiguration<Inclusions>
    {
        public void Configure(EntityTypeBuilder<Inclusions> builder)
        {
            // Table configuration
            builder.ToTable("Inclusions");

            // Primary key
            builder.HasKey(i => i.Id);

            // Property configurations
            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(i => i.VacationId)
                .HasMaxLength(100)
                .IsRequired(false);

            // Relationships
            builder.HasOne(i => i.Vacation)
                .WithMany(v => v.Inclusions)
                .HasForeignKey(i => i.VacationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(i => i.VacationId);
        }
    }
}