using RestaurantReviews.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace RestaurantReviews.Data
{
    public class RestaurantReviewsContext : DbContext
    {
        public RestaurantReviewsContext()
        {
            ((IObjectContextAdapter)this).ObjectContext.SavingChanges += (s, e) => SavingChanges?.Invoke(this);
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        public static event Action<DbContext> SavingChanges;
    }
}
