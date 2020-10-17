using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviewsService.ViewModels
{
    public class UserReviewViewModel
    {
        public int Id { get; set; }

        public int UserIdRef { get; set; }

        public int RestaurantIdRef { get; set; }

        public string Title { get; set; }

        public string Comments { get; set; }

        public int RatingsRef { get; set; }

        public int RatingsLevel { get; set; }

        public string RatingsDescription { get; set; } 

        public string RatingsRepresentation { get; set; }

        public UserViewModel UserViewModel { get; set; }
    }
}