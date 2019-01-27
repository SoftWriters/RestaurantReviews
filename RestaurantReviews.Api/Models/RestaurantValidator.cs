namespace RestaurantReviews.Api.Models
{
    public class RestaurantValidator : IRestaurantValidator
    {
        public bool IsRestaurantValid(NewRestaurant restaurant)
        {
            return !string.IsNullOrWhiteSpace(restaurant.Name) &&
                   !string.IsNullOrWhiteSpace(restaurant.City) &&
                   !string.IsNullOrWhiteSpace(restaurant.State) &&
                   !string.IsNullOrWhiteSpace(restaurant.Description);
        }
    }
}