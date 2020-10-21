using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Web.Data.Configuration;
using RestaurantReviews.Web.Data.Entities;

namespace RestaurantReviews.Web.Data
{
    public class RestaurantReviewsContext : DbContext
    {
        public RestaurantReviewsContext(DbContextOptions<RestaurantReviewsContext> options)
            : base(options) { }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RestaurantConfig());
            modelBuilder.ApplyConfiguration(new ReviewConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}
