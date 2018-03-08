using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.Domain
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int ReviewRestaurantId { get; set; } //Relationship between restaurant and review tables
        public ReviewUser ReviewUser { get; set; }
        public string ReviewContent { get; set; }
        public bool Active { get; set; }

    }

    public class ReviewUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}