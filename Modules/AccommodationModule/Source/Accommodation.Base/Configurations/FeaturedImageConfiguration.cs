using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the FeaturedImage entity.
    /// </summary>
    public class FeaturedImageConfiguration : IEntityTypeConfiguration<FeaturedImage>
    {
        public void Configure(EntityTypeBuilder<FeaturedImage> builder)
        {
            // Table configuration
            builder.ToTable("FeaturedImages");

            // Primary key
            builder.HasKey(fi => fi.Id);

            // Property configurations
            builder.Property(fi => fi.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(fi => fi.ImageType)
                .IsRequired();

            builder.Property(fi => fi.RoomId)
                .IsRequired(false);

            builder.Property(fi => fi.LodgingId)
                .IsRequired(false)
                .HasMaxLength(100);

            // Relationships
            builder.HasOne<Room>()
                .WithMany(r => r.FeaturedImages)
                .HasForeignKey(fi => fi.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Lodging>()
                .WithMany(l => l.FeaturedImages)
                .HasForeignKey(fi => fi.LodgingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(fi => fi.RoomId);
            builder.HasIndex(fi => fi.LodgingId);
        }
    }
}