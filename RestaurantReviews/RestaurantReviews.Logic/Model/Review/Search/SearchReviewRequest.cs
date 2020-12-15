using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Logic.Model.Review.Search

{
    public class SearchReviewRequest
    {
        public IEnumerable<string> UserIds { get; set; }
    }
}
