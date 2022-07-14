namespace RestaurantReviews.Api.Models
{
    public class RestaurantValidator : IRestaurantValidator
    {
        public bool IsRestaurantValid(NewRestaurant restaurant)
        {
            return !string.IsNullOrWhiteSpace(restaurant.Name) &&
                   !string.IsNullOrWhiteSpace(restaurant.City) &&
                   restaurant.State?.Length == 2 &&
                   !string.IsNullOrWhiteSpace(restaurant.Description);
        }
    }
}