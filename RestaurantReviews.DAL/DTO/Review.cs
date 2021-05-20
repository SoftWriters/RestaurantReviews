using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.DAL.DTO
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string RatingScore { get; set; }
        public int RestaurantId { get; set; }
        public string ReviewDescription { get; set; }
    }
}
