
using System;

namespace RestaurantReviews
{
    public class ReviewDTO
    {
        public long RestaurantID { get; set; }
        public long UserID { get; set; }
        public string Text { get; set; }
        public byte Rating { get; set; }
    }

    public class Review : ReviewDTO
    {
        public long ReviewID { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
