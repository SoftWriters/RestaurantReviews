using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Model.Review.Query
{
    public class ReviewQueryRequest
    {
        public IEnumerable<Guid> UserIds { get; set; }
    }
}
