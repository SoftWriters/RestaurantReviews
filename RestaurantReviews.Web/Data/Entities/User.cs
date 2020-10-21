using System.Collections.Generic;

namespace RestaurantReviews.Web.Data.Entities
{
    public class User
    {
        public int Id { get; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
