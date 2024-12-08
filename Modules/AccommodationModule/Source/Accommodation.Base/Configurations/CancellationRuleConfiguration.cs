using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the CancellationRule entity using EF Core fluent API.
    /// </summary>
    public class CancellationRuleConfiguration : IEntityTypeConfiguration<CancellationRule>
    {
        public void Configure(EntityTypeBuilder<CancellationRule> builder)
        {
            builder.ToTable("CancellationRules");

            builder.HasKey(cr => cr.Id);

            builder.Property(cr => cr.DaysBeforeBookingThatCancellationIsAvailable).IsRequired();

            // If CancellationFormualaType and Value are enums or strings, set constraints
            builder.Property(cr => cr.CancellationFormulaType).IsRequired();

            // Add indexing or foreign keys if needed
        }
    }
}