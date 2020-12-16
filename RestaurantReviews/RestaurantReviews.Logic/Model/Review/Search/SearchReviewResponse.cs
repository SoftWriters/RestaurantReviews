using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Logic.Model.Review.Search
{
    public class SearchReview
    {
        public string ReviewId { get; set; }
        public string ReviewText { get; set; }
        public string RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
