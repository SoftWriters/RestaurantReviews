using System.Data.Entity;
using RestaurantReviews.Data.EfLibrary.Entities;

namespace RestaurantReviews.Data.EfLibrary.Context
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
