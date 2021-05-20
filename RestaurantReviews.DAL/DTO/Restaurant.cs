using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.DAL.DTO
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string BusinessName { get; set; }
        public string PriceRatings { get; set; }
    }
}
