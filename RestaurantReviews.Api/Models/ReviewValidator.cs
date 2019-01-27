using RestaurantReviews.Api.DataAccess;

namespace RestaurantReviews.Api.Models
{
    public class ReviewValidator : IReviewValidator
    {
        private readonly IRestaurantQuery _restaurantQuery;

        public ReviewValidator(IRestaurantQuery restaurantQuery)
        {
            _restaurantQuery = restaurantQuery;
        }
        
        public bool IsReviewValid(NewReview review)
        {
            if (string.IsNullOrWhiteSpace(review.ReviewerEmail))
            {
                return false;
            }
            
            if (review.RatingStars < 0 || review.RatingStars > 5)
            {
                return false;
            }

            var restaurant = _restaurantQuery.GetRestaurant(review.RestaurantId).Result;
            if (restaurant == null)
            {
                return false;
            }

            return true;
        }
    }
}