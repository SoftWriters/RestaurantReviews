using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReviews.Model;

namespace RestaurantReviews.Data.Configurations
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A configuration object that specifies the properties for our EF Core data objects.
    ///             This gets called from our RestaurantReviewContext's OnModelCreating method.</summary>
    ///-------------------------------------------------------------------------------------------------

    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Url).HasMaxLength(250);
            builder.Property(x => x.Category).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Address1).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Address2).HasMaxLength(100);
            builder.Property(x => x.Address3).HasMaxLength(100);
            builder.Property(x => x.City).IsRequired().HasMaxLength(100);
            builder.Property(x => x.State).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(14);
            builder.Property(x => x.Price).IsRequired().HasMaxLength(5);
            builder.Property(x => x.Rating);
        }
    }
}