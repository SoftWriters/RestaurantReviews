using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ReviewModel : IReviewModel
    {
        public int Id
        { get; set; }

        public IUserModel SubmittingUser
        { get; set; }

        public IRestaurantModel Restaurant
        { get; set; }

        public int FoodRating
        { get; set; }

        public int CleanlinessRating
        { get; set; }

        public int ServiceRating
        { get; set; }

        public int AmbianceRating
        { get; set; }

        public int OverallRating
        { get; set; }

        public string Review
        { get; set; }
    }
}