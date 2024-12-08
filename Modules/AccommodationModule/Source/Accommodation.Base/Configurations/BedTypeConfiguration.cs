using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the BedType entity.
    /// </summary>
    public class BedTypeConfiguration : IEntityTypeConfiguration<BedType>
    {
        public void Configure(EntityTypeBuilder<BedType> builder)
        {
            // Table configuration
            builder.ToTable("BedTypes");

            // Primary key
            builder.HasKey(bt => bt.Id);

            // Property configurations
            builder.Property(bt => bt.PartnerBedTypeId)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(bt => bt.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(bt => bt.BedCount)
                .IsRequired();

            builder.Property(bt => bt.RoomId)
                .IsRequired(false);

            // Relationships
            builder.HasOne(bt => bt.Room)
                .WithMany(r => r.BedTypes)
                .HasForeignKey(bt => bt.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(bt => bt.RoomId);
        }
    }
}