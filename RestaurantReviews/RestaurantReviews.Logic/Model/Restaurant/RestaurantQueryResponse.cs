using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Logic.Model.Restaurant
{
    public class RestaurantQueryResponse
    {
        public IEnumerable<RestaurantQueryResponseRestaurant> Restaurants { get; set; }
    }

    public class RestaurantQueryResponseRestaurant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
