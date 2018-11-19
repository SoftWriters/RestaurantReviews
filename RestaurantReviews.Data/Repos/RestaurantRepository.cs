using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Model;
using RestaurantReviews.Data.Interfaces;

namespace RestaurantReviews.Data.Repos
{
    public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
    {
        private readonly RestaurantReviewContext _dbContext;

        public RestaurantRepository(RestaurantReviewContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Restaurant> GetRestaurantsByCity(string city)
        {
            return _dbContext.Restaurants.Where(x => x.City == city);
        }
    }

}