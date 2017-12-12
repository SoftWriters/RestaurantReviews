using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RestaurantReviews.Models
{
    public class RestaurantReviewContext : DbContext
    {
        public RestaurantReviewContext(DbContextOptions<RestaurantReviewContext> options)
            : base(options)
        {
            BuildDB();
        }
        
        ///<summary>
        ///Ensure unique address when creating a new restaurant
        ///</summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Restaurant>()
                .HasAlternateKey(c => new {c.Street, c.City, c.State})
                .HasName("AlternateKey_Street_City_State");
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public void BuildDB()
        {
            if (Restaurants.Count() == 0 && initDone_ == false)
            {
                Restaurants.Add(new Restaurant {
                    Name = "Eat'n Park", 
                    Street = "1197 Washington Pike",
                    City = "Bridgeville",
                    State = "PA",
                    Zip = "15017"});
                Restaurants.Add(new Restaurant {
                    Name = "Shouf's Cafe", 
                    Street = "200 Washington Ave",
                    City = "Bridgeville",
                    State = "PA",
                    Zip = "15017"});
                Restaurants.Add(new Restaurant {
                    Name = "Little Tokyo", 
                    Street = "636 Washington Rd",
                    City = "Pittsburgh",
                    State = "PA",
                    Zip = "15228"});

                SaveChanges();
            }

            if (Reviews.Count() == 0 && initDone_ == false)
            {
                Reviews.Add(new Review { UserName = "demouser1",
                    Rating = 4,
                    Description = "Yummy sushi blah blah",
                    RestaurantId = 3});
                Reviews.Add(new Review { UserName = "demouser2",
                    Rating = 5,
                    Description = "Yummy Lebanese food blah blah",
                    RestaurantId = 2});
                
                SaveChanges();
            }
            

            initDone_ = true;
        }

        static bool initDone_ = false;
    }
}