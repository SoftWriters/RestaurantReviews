using Microsoft.EntityFrameworkCore;

namespace RestaurantReviews.Models
{
    public class RestaurantReviewContext : DbContext
    {
        public RestaurantReviewContext(DbContextOptions<RestaurantReviewContext> options)
            : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}