using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReviews.Web.Data.Entities;

namespace RestaurantReviews.Web.Data.Configuration
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews", "dbo");
            builder.HasKey(r => r.Id);
            builder.HasOne(r => r.Restaurant);
            builder.Property(r => r.Id).HasColumnName("ReviewId");
            builder.Property(r => r.Active).HasDefaultValue();
            builder.Property(r => r.DateCreated).HasDefaultValue();
        }
    }
}
