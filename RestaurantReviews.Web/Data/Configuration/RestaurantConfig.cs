using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReviews.Web.Data.Entities;

namespace RestaurantReviews.Web.Data.Configuration
{
    public class RestaurantConfig : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurants", "dbo");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasColumnName("RestaurantId");
            builder.Property(r => r.Name).HasMaxLength(255);
            builder.Property(r => r.Address).HasMaxLength(255);
            builder.Property(r => r.City).HasMaxLength(255);
            builder.Property(r => r.State).HasMaxLength(2);
            builder.Property(r => r.ZipCode).HasMaxLength(10);
        }
    }
}
