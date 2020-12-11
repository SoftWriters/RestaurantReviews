using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Logic.Model.Review.Query
{
    public class ReviewQueryRequest
    {
        public IEnumerable<Guid> UserIds { get; set; }
    }
}
