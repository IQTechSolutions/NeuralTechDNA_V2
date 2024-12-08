using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the AvailablePartner entity.
    /// </summary>
    public class AvailablePartnerConfiguration : IEntityTypeConfiguration<AvailablePartner>
    {
        public void Configure(EntityTypeBuilder<AvailablePartner> builder)
        {
            // Table configuration
            builder.ToTable("AvailablePartners");

            // Primary key
            builder.HasKey(ap => ap.Id);

            // Property configurations
            builder.Property(ap => ap.PartnerName)
                .IsRequired()
                .HasMaxLength(200);

            // Relationships
            builder.HasMany(ap => ap.Services)
                .WithOne(s => s.AvailablePartner)
                .HasForeignKey(s => s.AvailablePartnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ap => ap.AccountTypes)
                .WithOne()
                .HasForeignKey(p => p.AvailablePartnerUid)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(ap => ap.PartnerName);
        }
    }
}