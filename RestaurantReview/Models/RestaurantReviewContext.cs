using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantReview.Models
{
    public class RestaurantReviewContext : DbContext
    {
       
        // reference the DB Connection
        public RestaurantReviewContext() : base("name=RestaurantReviewContext")
        {
        }

        public DbSet<Restaurants> Restaurants { get; set; }
    }
}
