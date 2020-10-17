using RestaurantReviewsService.ViewModels;
using System.Collections.Generic;

namespace RestaurantReviewsService.PortServices
{
    public interface IRestaurantPortService
    {
        IList<RestaurantViewModel> GetAllRestaurants();

        IList<RestaurantViewModel> GetRestaurantReviewsByCity(string cityName);

        void PostRestaurantReview(UserReviewViewModel userReviewViewModel);

        void AddRestaurant(RestaurantViewModel restaurantViewModel);
    }
}

