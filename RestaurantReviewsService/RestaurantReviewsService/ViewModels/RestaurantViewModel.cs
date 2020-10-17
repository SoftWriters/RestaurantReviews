using System.Collections.Generic;

namespace RestaurantReviewsService.ViewModels
{
    public class RestaurantViewModel
    {
        public RestaurantViewModel()
        {
            UserReviews = new List<UserReviewViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int StateId { get; set; }

        public string Overview { get; set; }

        public IList<UserReviewViewModel> UserReviews { get; set; }
    }
}