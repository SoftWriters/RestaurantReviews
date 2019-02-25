using RestaurantReviews.Data.Entities;
using RestaurantReviews.Data.ExtendedModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.Contracts.Repositories.Entities
{
    public interface IRestaurantRepository : IRepositoryBase<Restaurant>
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurants();

        Task<IEnumerable<Restaurant>> GetRestaurantsByCity(string city, string state, string country);

        Task<IEnumerable<Restaurant>> GetRestaurantsByCondition(Expression<Func<Restaurant, bool>> expression);

        Task<Restaurant> GetRestaurantById(Guid schoolId);

        Task<RestaurantExtended> GetRestaurantWithDetails(Guid schoolId);

        Task CreateRestaurant(Restaurant restaurant);

        Task UpdateRestaurant(Restaurant dbRestaurant, Restaurant restaurant);

        Task DeleteRestaurant(Restaurant restaurant);
    }
}
