using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.API.DTOs
{
    public class ReviewDTO
    {
        public Guid ReviewId { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid ResturantId { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewComment { get; set; }
        public int Rating { get; set; }// 1-5
        public DateTime CreatedTime { get; set; }
    }
}
