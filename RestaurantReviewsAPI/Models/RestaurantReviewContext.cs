using Microsoft.EntityFrameworkCore;

namespace RestaurantReviewsAPI.Models
{
    public class RestaurantReviewContext : DbContext
    {
        public RestaurantReviewContext(DbContextOptions<RestaurantReviewContext> options)
            : base(options)
        {
        }

        public DbSet<RestaurantReview> RestaurantReviewItems { get; set; }
    }
}