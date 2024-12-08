using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the Rates entity.
    /// </summary>
    public class RatesConfiguration : IEntityTypeConfiguration<Rates>
    {
        public void Configure(EntityTypeBuilder<Rates> builder)
        {
            // Table configuration
            builder.ToTable("Rates");

            // Primary key
            builder.HasKey(r => r.Id);

            // Property configurations
            builder.Property(r => r.SingleRoom)
                .IsRequired(false);

            builder.Property(r => r.SingleRoomRate)
                .HasColumnType("double")
                .IsRequired(false);

            builder.Property(r => r.RateCodeSingle)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(r => r.HasValueSingle)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(r => r.DoubleRoom)
                .IsRequired(false);

            builder.Property(r => r.DoubleRoomRate)
                .HasColumnType("double")
                .IsRequired(false);

            builder.Property(r => r.RateCodeDouble)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(r => r.HasValueDouble)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(r => r.AvailableDate)
                .HasColumnType("date")
                .IsRequired(false);

            builder.Property(r => r.TpnUid)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);

            // Foreign key and relationship with Service
            builder.HasOne(r => r.Service)
                .WithMany(s => s.Rates)
                .HasForeignKey(r => r.ServiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            // Index configurations
            builder.HasIndex(r => r.TpnUid)
                .IsUnique();
        }
    }
}
