using System.Collections.Generic;

namespace Softwriters.RestaurantReviews.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
