using RestaurantReviews.Web.Data.Entities;

namespace RestaurantReviews.Web
{
    public class RestaurantNotFoundException : NotFoundException<Restaurant>
    {
        public RestaurantNotFoundException() { }

        public RestaurantNotFoundException(int restaurantId)
        {
            _message = $"No restaurant found for ID { restaurantId }";
        }
    }
}
