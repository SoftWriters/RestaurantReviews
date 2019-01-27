namespace RestaurantReviews.Api.Models
{
    public interface IRestaurantValidator
    {
        bool IsRestaurantValid(NewRestaurant restaurant);
    }
}