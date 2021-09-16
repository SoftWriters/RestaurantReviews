using Softwriters.RestaurantReviews.PrivateModels;
using System;

namespace Softwriters.RestaurantReviews.Dto
{
    public class ReviewRequest : EntityBase
    {
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int RestaurantId { get; set; }

        public double Stars { get; set; }

        public int UserId { get; set; }
    }
}
