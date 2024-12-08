using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the ChildPolicyRule entity.
    /// </summary>
    public class ChildPolicyRuleConfiguration : IEntityTypeConfiguration<ChildPolicyRule>
    {
        public void Configure(EntityTypeBuilder<ChildPolicyRule> builder)
        {
            // Table configuration
            builder.ToTable("ChildPolicyRules");

            // Primary key
            builder.HasKey(cpr => cpr.Id);

            // Property configurations
            builder.Property(cpr => cpr.MinAge)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnType("int");

            builder.Property(cpr => cpr.MaxAge)
                .IsRequired()
                .HasDefaultValue(100)
                .HasColumnType("int");

            builder.Property(cpr => cpr.Allowed)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(cpr => cpr.UseSpecialRate)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(cpr => cpr.ChildPolicyFormualaType)
                .IsRequired()
                .HasMaxLength(1)
                .HasDefaultValue("N");

            builder.Property(cpr => cpr.ChildPolicyFormualaValue)
                .IsRequired()
                .HasDefaultValue(1.0)
                .HasColumnType("double");

            builder.Property(cpr => cpr.CustomDescription)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(cpr => cpr.RoomId)
                .IsRequired(false);

            // Relationships
            builder.HasOne(cpr => cpr.Room)
                .WithMany(r => r.ChildPolicyRules)
                .HasForeignKey(cpr => cpr.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(cpr => cpr.RoomId);
        }
    }
}


