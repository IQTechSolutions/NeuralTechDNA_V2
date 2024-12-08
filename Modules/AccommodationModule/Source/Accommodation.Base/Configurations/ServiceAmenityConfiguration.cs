using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the ServiceAmenity entity.
    /// </summary>
    public class ServiceAmenityConfiguration : IEntityTypeConfiguration<ServiceAmenity>
    {
        public void Configure(EntityTypeBuilder<ServiceAmenity> builder)
        {
            // Table configuration
            builder.ToTable("ServiceAmenities");

            // Primary key
            builder.HasKey(sa => sa.Id);

            // Property configurations
            builder.Property(sa => sa.Name)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(sa => sa.RoomId)
                .IsRequired();

            // Relationships

            // Room relationship
            builder.HasOne<Room>()
                .WithMany(r => r.Amneties)
                .HasForeignKey(sa => sa.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}