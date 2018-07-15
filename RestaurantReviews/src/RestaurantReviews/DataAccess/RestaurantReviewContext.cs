using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestaurantReviews.Models;

namespace RestaurantReviews.DataAccess
{
    public class RestaurantReviewContext : DbContext
    {
        public RestaurantReviewContext()
        {

        }
        public RestaurantReviewContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Review> Review { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasIndex(address => address.ZipCode);
            modelBuilder.Entity<Address>().HasIndex(address => address.City);
            modelBuilder.Entity<User>().HasIndex(user => user.UserName);
            modelBuilder.Entity<Contact>().HasIndex(contact => contact.Email);
            modelBuilder.Entity<Restaurant>().HasIndex(restaurant => restaurant.Name);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("RestaurantReviews"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
