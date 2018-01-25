using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework;

namespace RestaurantReviews.EntityFramework
{
    public class RestaurantRepository : RestaurantReviewsRepositoryBase<Restaurant, long>, IRestaurantRepository
    {
        public List<Restaurant> GetAllByCity(int? cityId)
        {
            var query = Context.Restaurants.AsQueryable();

            if (cityId.HasValue)
            {
                query = query.Where(restaurant => restaurant.CityId == cityId.Value);
            }

            return query
                .OrderByDescending(restaurant => restaurant.CreationTime)
                .ToList();
        }

        protected RestaurantRepository(IDbContextProvider<RestaurantReviewsDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }
    }
}
