using Microsoft.EntityFrameworkCore;
using Softwriters.RestaurantReviews.Models.Entities;

namespace Softwriters.RestaurantReviews.Data.DataContext
{
    public class ReviewsContext : DbContext
    {
        public ReviewsContext(DbContextOptions<ReviewsContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantType> RestaurantTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Critic> Critics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable(nameof(City));
            modelBuilder.Entity<Menu>().ToTable(nameof(Menu));
            modelBuilder.Entity<Restaurant>().ToTable(nameof(Restaurant));
            modelBuilder.Entity<RestaurantType>().ToTable(nameof(RestaurantType));
            modelBuilder.Entity<Review>().ToTable(nameof(Review));
            modelBuilder.Entity<Critic>().ToTable(nameof(Critic));
        }
    }
}
