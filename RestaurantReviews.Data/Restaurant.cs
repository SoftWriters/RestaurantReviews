using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Data
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public List<Review> Reviews { get; } = new List<Review>();
    }
}
