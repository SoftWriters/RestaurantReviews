using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Logic.Model.Restaurant.Search
{
    public class SearchRestaurant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
