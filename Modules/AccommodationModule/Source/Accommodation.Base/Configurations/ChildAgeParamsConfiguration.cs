using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the ChildAgeParams entity.
    /// </summary>
    public class ChildAgeParamsConfiguration : IEntityTypeConfiguration<ChildAgeParams>
    {
        public void Configure(EntityTypeBuilder<ChildAgeParams> builder)
        {
            // Table configuration
            builder.ToTable("ChildAgeParams");

            // Primary key
            builder.HasKey(cap => cap.Id);

            // Property configurations
            builder.Property(cap => cap.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(cap => cap.Type)
                .IsRequired();

            builder.Property(cap => cap.Value)
                .IsRequired(false);

            builder.Property(cap => cap.UniqueNr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cap => cap.ServiceId)
                .IsRequired();

            // Relationships
            builder.HasOne(cap => cap.Service)
                .WithMany(s => s.ChildAgeParams)
                .HasForeignKey(cap => cap.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(cap => cap.ServiceId);
        }
    }
}