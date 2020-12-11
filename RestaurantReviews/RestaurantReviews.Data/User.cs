using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Data
{
    public class User : Entity
    {
        public string First { get; set; }
        public string Last { get; set; }
        
        public IList<Review> Reviews { get; set; }
    }
}