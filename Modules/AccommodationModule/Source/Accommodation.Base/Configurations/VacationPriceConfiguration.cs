using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the VacationPrice entity.
    /// </summary>
    public class VacationPriceConfiguration : IEntityTypeConfiguration<VacationPrice>
    {
        public void Configure(EntityTypeBuilder<VacationPrice> builder)
        {
            // Table configuration
            builder.ToTable("VacationPrices");

            // Primary key
            builder.HasKey(vp => vp.Id);

            // Property configurations
            builder.Property(vp => vp.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(vp => vp.Description)
                .HasMaxLength(5000);

            builder.Property(vp => vp.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(vp => vp.VacationId)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(vp => vp.Vacation)
                .WithMany(v => v.Prices)
                .HasForeignKey(vp => vp.VacationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
