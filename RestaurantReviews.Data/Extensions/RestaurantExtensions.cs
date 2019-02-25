using RestaurantReviews.Data.Entities;

namespace RestaurantReviews.Data.Extensions
{
    public static class RestaurantExtensions
    {
        public static void Map(this Restaurant dbRestaurant, Restaurant restaurant)
        {
            dbRestaurant.Address = restaurant.Address;
            dbRestaurant.EmailAddress = restaurant.EmailAddress;
            dbRestaurant.IsConfirmed = restaurant.IsConfirmed;
            dbRestaurant.Name = restaurant.Name;
            dbRestaurant.Phone = restaurant.Phone;
            dbRestaurant.WebsiteUrl = restaurant.WebsiteUrl;
        }
    }
}
