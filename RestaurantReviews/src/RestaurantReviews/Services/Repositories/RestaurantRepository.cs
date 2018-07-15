using System.Collections.Generic;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Models;
using RestaurantReviews.Repositories.Interfaces;
using System.Linq;

namespace RestaurantReviews.Services.Repositories
{
    /// <summary>
    /// Restaurant repository.
    /// </summary>
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(RestaurantReviewContext context) : base(context)
        {
        }

        public IEnumerable<Restaurant> GetRestaurantsByAddress(RestaurantRequest address)
        {
            if (address == null) return null;
            return context.Restaurant.Where(restaurant => 
                (restaurant.ContactInformation.Address.ZipCode == address.ZipCode && address.ZipCode != null) ||
                (restaurant.ContactInformation.Address.City == address.City && address.City != null)).ToList();
        }

        public Restaurant GetRestaurantById(long restaurantId)
        {
            return context.Restaurant.Where(restaurant => restaurant.RestaurantId == restaurantId).FirstOrDefault();
        }

        public Restaurant GetRestaurantByProperties(Restaurant restaurauntToFind)
        {
            return context.Restaurant.Where(restaurant => (restaurauntToFind.Name == restaurant.Name
            && restaurant.ContactInformation.Address.City == restaurauntToFind.ContactInformation.Address.City) || 
            (restaurant.Name == restaurauntToFind.Name && restaurant.ContactInformation.Address.ZipCode == restaurauntToFind.ContactInformation.Address.ZipCode)).FirstOrDefault();
        }
    }
}