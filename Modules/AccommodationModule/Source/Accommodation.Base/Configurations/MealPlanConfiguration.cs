using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the MealPlan entity.
    /// </summary>
    public class MealPlanConfiguration : IEntityTypeConfiguration<MealPlan>
    {
        public void Configure(EntityTypeBuilder<MealPlan> builder)
        {
            // Table configuration
            builder.ToTable("MealPlans");

            // Primary key
            builder.HasKey(mp => mp.Id);

            // Property configurations
            builder.Property(mp => mp.RoomId)
                .IsRequired();

            builder.Property(mp => mp.RateId)
                .IsRequired();

            builder.Property(mp => mp.PartnerMealPlanId)
                .IsRequired()
                .HasConversion<int>(); // Stores enum as integer

            builder.Property(mp => mp.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(mp => mp.Default)
                .IsRequired();

            builder.Property(mp => mp.Rate)
                .IsRequired()
                .HasColumnType("double");

            builder.Property(mp => mp.OriginalRate)
                .IsRequired()
                .HasColumnType("double");

            // Relationships

            // Room relationship
            builder.HasOne<Room>()
                .WithMany(r => r.MealPlans) 
                .HasForeignKey(mp => mp.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Rate relationship
            builder.HasOne<Rates>() 
                .WithMany() 
                .HasForeignKey(mp => mp.RateId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index configurations
            builder.HasIndex(mp => mp.PartnerMealPlanId);
            builder.HasIndex(mp => mp.RoomId);
            builder.HasIndex(mp => mp.RateId);
        }
    }
}
