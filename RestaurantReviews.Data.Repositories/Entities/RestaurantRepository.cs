using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data.Contracts.Repositories.Entities;
using RestaurantReviews.Data.Entities;
using RestaurantReviews.Data.ExtendedModels;
using RestaurantReviews.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.Repositories.Entities
{
    public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(RestaurantReviewsContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            return await this.RepositoryContext.Set<Restaurant>().OrderBy(ow => ow.Name).ToListAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantsByCity(string city, string state, string country)
        {
            return await GetRestaurantsByCondition(restaurant => restaurant.City == city && restaurant.State == state && restaurant.Country == country);
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantsByCondition(Expression<Func<Restaurant, bool>> expression)
        {
            return await this.RepositoryContext.Set<Restaurant>().Where(expression).ToListAsync();
        }

        public async Task<Restaurant> GetRestaurantById(Guid schoolId)
        {
            return (await FindByCondition(student => student.Id.Equals(schoolId)))
                    .DefaultIfEmpty(new Restaurant())
                    .FirstOrDefault();
        }

        public async Task<RestaurantExtended> GetRestaurantWithDetails(Guid restaurantId)
        {
            return new RestaurantExtended(await GetRestaurantById(restaurantId))
            {
                Reviews = RepositoryContext.Reviews.Where(a => a.RestaurauntId == restaurantId)
            };
        }

        public async Task CreateRestaurant(Restaurant school)
        {
            school.Id = Guid.NewGuid();
            await Create(school);
            await Save();
        }

        public async Task UpdateRestaurant(Restaurant dbRestaurant, Restaurant restaurant)
        {
            dbRestaurant.Map(restaurant);
            await Update(dbRestaurant);
            await Save();
        }

        public async Task DeleteRestaurant(Restaurant restaurant)
        {
            await Delete(restaurant);
            await Save();
        }
    }
}
