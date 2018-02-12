using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.EfLibrary.Entities;

namespace RestaurantReviews.Data.EfLibrary
{
    public class RestaurantReviewsContext : DbContext
    {
        public RestaurantReviewsContext() : base("DefaultConnection")
        {
        }

        public IDbSet<UserDBO> Users { get; set; }
        public IDbSet<StateDBO> States { get; set; }
        public IDbSet<RestaurantDBO> Restaurants { get; set; }
    }
}
