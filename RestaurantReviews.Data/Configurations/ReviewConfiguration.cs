using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReviews.Model;

namespace RestaurantReviews.Data.Configurations
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A configuration object that specifies the properties for our EF Core data objects.
    ///             This gets called from our RestaurantReviewContext's OnModelCreating method.</summary>
    ///-------------------------------------------------------------------------------------------------

    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.RestaurantId).IsRequired();
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.Text).IsRequired().HasMaxLength(50);
            builder.Property(x => x.WhenCreated).ValueGeneratedOnAdd();
        }
    }
}