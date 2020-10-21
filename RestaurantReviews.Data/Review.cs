using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Data
{
    public class Review
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
