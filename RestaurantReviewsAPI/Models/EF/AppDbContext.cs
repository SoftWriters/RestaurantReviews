using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReviewsAPI.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<MobileUser> MobileUsers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
