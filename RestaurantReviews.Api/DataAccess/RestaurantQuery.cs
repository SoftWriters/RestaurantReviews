using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api.DataAccess
{
    public class RestaurantQuery : IRestaurantQuery
    {
        private const string BaseQuery = "SELECT * FROM dbo.Restaurant";
        
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Restaurant>> GetRestaurants(string city=null, string state=null)
        {
            string query;
            object parameters;
       
            if (city == null)
            {
                query = BaseQuery;
                parameters = null;
            }
            else
            {
                query = BaseQuery + " WHERE City = @City AND State = @State";
                parameters = new { City = city, State = state };
            }
            
            var result = await _unitOfWork.Connection.QueryAsync<Restaurant>(query, parameters);
            return result.ToList();
        }
    }
}