using System;
using System.Collections.Generic;

namespace RestaurantReviews.API.Data.SqlServer.DataModel
{
    public partial class TblRestaurant
    {
        public TblRestaurant()
        {
            TblReview = new HashSet<TblReview>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public ICollection<TblReview> TblReview { get; set; }
    }
}
