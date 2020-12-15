using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Logic.Model.Restaurant.Search
{
    public class SearchRestaurantRequest
    {
        public string State { get; set; }
        public IEnumerable<string> Cities { get; set; }
    }
}
