using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Common.Configuration;
using RestaurantReviews.Data.DataSeeding;
using RestaurantReviews.Data.Entities;

namespace RestaurantReviews.Data
{
    public class RestaurantReviewsContext : DbContext
    {
        #region Constructors

        public RestaurantReviewsContext()
        {
        }

        #endregion Constructors

        #region Configuration

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // ToDo: Refactor this into secret or appsettings file
                optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=RestaurantReviews;User Id=postgres;Password=password;");
            }
        }

        #endregion Configuration

        #region DbSets

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion DbSets

        #region Data Seeding

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var user in DataSeeder.Users) { modelBuilder.Entity<User>().HasData(user); }
            foreach (var restaurant in DataSeeder.Restaurants) { modelBuilder.Entity<Restaurant>().HasData(restaurant); }
            foreach (var review in DataSeeder.Reviews) { modelBuilder.Entity<Review>().HasData(review); }
        }

        #endregion Data Seeding
    }
}
