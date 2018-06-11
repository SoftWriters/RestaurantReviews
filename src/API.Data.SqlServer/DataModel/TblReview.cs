using System;
using System.Collections.Generic;

namespace RestaurantReviews.API.Data.SqlServer.DataModel
{
    public partial class TblReview
    {
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime ReviewDateTime { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }

        public TblRestaurant Restaurant { get; set; }
        public TblReviewer Reviewer { get; set; }
    }
}
