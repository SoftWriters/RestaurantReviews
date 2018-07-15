using RestaurantReviews.DataAccess;
using RestaurantReviews.Interfaces;
using RestaurantReviews.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Services
{
    public class RestaurantFacade
    {

        public IEnumerable<Restaurant> GetAllRestaurantsByAddress(RestaurantRequest request)
        {
            using (var context = new RestaurantReviewContext())
            {
                IUnitOfWork unitOfWork = new UnitOfWork(context);
                return unitOfWork.Restaurants.GetRestaurantsByAddress(request);
            }
        }

        public Restaurant GetRestaurantById(long restaurantId)
        {
            using (var context = new RestaurantReviewContext())
            {
                IUnitOfWork unitOfWork = new UnitOfWork(context);
                return unitOfWork.Restaurants.GetRestaurantById(restaurantId);
            }
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            using (var context = new RestaurantReviewContext())
            {
                IUnitOfWork unitOfWork = new UnitOfWork(context);
                unitOfWork.Restaurants.Add(restaurant);
                unitOfWork.Save();
            }
        }

        internal Restaurant GetExistingRestaurant(Restaurant restaurant)
        {
            using (var context = new RestaurantReviewContext())
            {
                IUnitOfWork unitOfWork = new UnitOfWork(context);
                return unitOfWork.Restaurants.GetRestaurantByProperties(restaurant);
            }
        }
    }
}
