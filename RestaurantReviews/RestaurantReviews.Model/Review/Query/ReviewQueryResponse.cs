using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Model.Review.Query
{
    public class ReviewQueryResponse
    {
        public IEnumerable<ReviewQueryResponseReview> Reviews { get; set; }
    }

    public class ReviewQueryResponseReview
    {
        public string ReviewId { get; set; }
        public string ReviewText { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
