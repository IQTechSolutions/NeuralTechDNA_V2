using Accommodation.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommodation.Base.Configurations
{
    /// <summary>
    /// Configures the Lodging entity.
    /// </summary>
    public class LodgingConfiguration : IEntityTypeConfiguration<Lodging>
    {
        public void Configure(EntityTypeBuilder<Lodging> builder)
        {
            // Table configuration
            builder.ToTable("Lodgings");

            // Primary key
            builder.HasKey(l => l.Id);

            // Property configurations
            builder.Property(l => l.UniquePartnerId)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(l => l.CoverImageUrl)
                .IsRequired()
                .HasMaxLength(500)
                .HasDefaultValue("_content/Accomodation.Blazor/images/NoImage.jpg");

            builder.Property(l => l.Name)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(l => l.Description)
                .HasMaxLength(5000)
                .IsRequired(false);

            builder.Property(l => l.RoomInformation)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(l => l.Teaser)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(l => l.Facilities)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(l => l.Attractions)
                .HasMaxLength(1000)
                .IsRequired(false);

            // Policies
            builder.Property(l => l.TermsAndConditions)
                .HasMaxLength(2000)
                .IsRequired(false);

            builder.Property(l => l.DepositPolicy)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(l => l.LowestGuestAgeCutOff)
                .IsRequired();

            builder.Property(l => l.MiddleGuestAgeCutOff)
                .IsRequired();

            builder.Property(l => l.HighestGuestAgeCutOff)
                .IsRequired();

            // Pricing
            builder.Property(l => l.DefaultCommissionPercentage)
                .IsRequired()
                .HasDefaultValue(20)
                .HasColumnType("double");

            builder.Property(l => l.DefaultMarkupPercentage)
                .IsRequired()
                .HasDefaultValue(4)
                .HasColumnType("double");

            builder.Property(l => l.Discount)
                .IsRequired()
                .HasColumnType("double")
                .HasDefaultValue(0);

            builder.Property(l => l.Rate)
                .IsRequired()
                .HasColumnType("double")
                .HasDefaultValue(0);

            builder.Property(l => l.DefaultRateScheme)
                .IsRequired();

            // Contact Information
            builder.Property(l => l.Contacts)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(l => l.PhoneNr)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(l => l.CellNr)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(l => l.Email)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(l => l.Website)
                .HasMaxLength(200)
                .IsRequired(false);

            // Page Details
            builder.Property(l => l.PageTitle)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(l => l.MetaKeys)
                .HasMaxLength(5000)
                .IsRequired(false);

            builder.Property(l => l.MetaDescription)
                .HasMaxLength(500)
                .IsRequired(false);

            // Location
            builder.Property(l => l.AreaId)
                .IsRequired();

            builder.Property(l => l.AreaInfo)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(l => l.Address)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(l => l.Suburb)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(l => l.City)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(l => l.Lat)
                .IsRequired()
                .HasColumnType("decimal(9,6)");

            builder.Property(l => l.Lng)
                .IsRequired()
                .HasColumnType("decimal(9,6)");

            builder.Property(l => l.Zoom)
                .IsRequired()
                .HasDefaultValue(10);

            builder.Property(l => l.Directions)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(l => l.ProvinceId)
                .IsRequired(false);

            // Relationships

            // Settings relationship (One-to-One)
            builder.HasOne(l => l.Settings)
                .WithOne()
                .HasForeignKey<LodgingSettings>(ls => ls.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // CancellationRules relationship (One-to-Many)
            builder.HasMany(l => l.CancellationRules)
                .WithOne(cr => cr.Lodging)
                .HasForeignKey(cr => cr.LodgingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Categories relationship (Many-to-Many via EntityCategory<Lodging>)
            builder.HasMany(l => l.Categories)
                .WithOne(ec => ec.Entity)
                .HasForeignKey(ec => ec.EntityId)
                .OnDelete(DeleteBehavior.Cascade);

            // AccountTypes relationship (One-to-Many)
            builder.HasMany(l => l.AccountTypes)
                .WithOne(p => p.Lodging)
                .HasForeignKey(p => p.LodgingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Amenities relationship (One-to-Many)
            builder.HasMany(l => l.Amenities)
                .WithOne()
                .HasForeignKey(ai => ai.AmenityId)
                .OnDelete(DeleteBehavior.Cascade);

            // Vouchers relationship (One-to-Many)
            builder.HasMany(l => l.Vouchers)
                .WithOne(v => v.Lodging)
                .HasForeignKey(v => v.LodgingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Services relationship (One-to-Many)
            builder.HasMany(l => l.Services)
                .WithOne(s => s.Lodging)
                .HasForeignKey(s => s.LodgingId)
                .OnDelete(DeleteBehavior.Cascade);

            // FeaturedImages relationship (One-to-Many)
            builder.HasMany(l => l.FeaturedImages)
                .WithOne()
                .HasForeignKey(fi => fi.LodgingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Vacations relationship (One-to-Many)
            builder.HasMany(l => l.Vacations)
                .WithOne(vl => vl.Lodging)
                .HasForeignKey(vl => vl.LodgingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index configurations
            builder.HasIndex(l => l.UniquePartnerId).IsUnique(false);
            builder.HasIndex(l => l.Name).IsUnique(false);
            builder.HasIndex(l => l.AreaId);
            builder.HasIndex(l => l.ProvinceId);
            builder.HasIndex(l => l.Email);
            builder.HasIndex(l => l.PhoneNr);
            builder.HasIndex(l => l.City);
        }
    }
}
