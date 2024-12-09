using Beneficiary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beneficiary.Configurations
{
    /// <summary>
    /// Configures the Ambassador entity using EF Core's fluent API.
    /// Defines table mapping, property constraints, and relationships.
    /// </summary>
    public class AmbassadorConfiguration : IEntityTypeConfiguration<Ambassador>
    {
        public void Configure(EntityTypeBuilder<Ambassador> builder)
        {
            builder.ToTable("Ambassadors");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Surname).IsRequired().HasMaxLength(200);
            builder.Property(a => a.PhoneNr).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(200);

            // If needed, define precision for CommissionPercentage:
            // builder.Property(a => a.CommissionPercentage).HasPrecision(5,2);

            // Relationship with Beneficiaries
            builder.HasMany(a => a.Beneficiaries)
                .WithOne(b => b.Ambassador)
                .HasForeignKey(b => b.AmbassadorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}