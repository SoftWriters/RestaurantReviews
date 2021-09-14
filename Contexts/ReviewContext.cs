
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReviews
{
    public class ReviewContext : DbContext
    {
        public ReviewContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var filename = Directory.GetCurrentDirectory();
                filename += "\\SQLite\\RestaurantReviews.db";
                optionsBuilder.UseSqlite("Filename=" + filename);
            }
        }
    }
}
