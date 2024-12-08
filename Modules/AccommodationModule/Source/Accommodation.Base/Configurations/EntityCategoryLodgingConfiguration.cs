using Accommodation.Base.Entities;
using Grouping.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the EntityCategory<Lodging> entity using EF Core fluent API.
    /// </summary>
    public class EntityCategoryLodgingConfiguration : IEntityTypeConfiguration<EntityCategory<Lodging>>
    {
        public void Configure(EntityTypeBuilder<EntityCategory<Lodging>> builder)
        {
            builder.ToTable("EntityCategories_Lodging");

            builder.HasKey(ec => ec.Id);

            // Assume EntityCategory has a reference to Lodging:
            builder.HasOne(ec => ec.Entity)
                .WithMany(l => l.Categories)
                .HasForeignKey(ec => ec.EntityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
