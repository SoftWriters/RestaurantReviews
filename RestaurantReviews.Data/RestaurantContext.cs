using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Data
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            : base(options)
        {
        }
    }
}
