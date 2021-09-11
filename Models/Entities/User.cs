using System.Collections.Generic;

namespace Softwriters.RestaurantReviews.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
