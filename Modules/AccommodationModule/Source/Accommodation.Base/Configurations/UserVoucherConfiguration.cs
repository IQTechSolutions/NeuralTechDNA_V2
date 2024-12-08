using Accommodation.Base.Entities.Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the UserVoucher entity.
    /// </summary>
    public class UserVoucherConfiguration : IEntityTypeConfiguration<UserVoucher>
    {
        public void Configure(EntityTypeBuilder<UserVoucher> builder)
        {
            // Table configuration
            builder.ToTable("UserVouchers");

            // Primary key
            builder.HasKey(uv => uv.Id);

            // Property configurations
            builder.Property(uv => uv.UserId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(uv => uv.VoucherId)
                .IsRequired();

            builder.Property(uv => uv.RoomId)
                .IsRequired();

            builder.Property(uv => uv.OrderId)
                .HasMaxLength(100);

            // Relationships

            // Voucher relationship
            builder.HasOne(uv => uv.Voucher)
                .WithMany(v => v.Vouchers)
                .HasForeignKey(uv => uv.VoucherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Room relationship
            builder.HasOne(uv => uv.Room)
                .WithMany()
                .HasForeignKey(uv => uv.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order relationship
            builder.HasOne(uv => uv.Order)
                .WithMany(o => o.Vouchers)
                .HasForeignKey(uv => uv.OrderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}