using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api.DataAccess
{
    public class InsertRestaurant : IInsertRestaurant
    {
        private const string BaseQuery = @"
            INSERT INTO [dbo].[Restaurant] (
                [Name]
                ,[Description]
                ,[City]
                ,[State])
            VALUES (
                @Name,
                @Description,
                @City,
                @State)
                
            SELECT CAST(SCOPE_IDENTITY() as int)";
        
        private readonly IUnitOfWork _unitOfWork;

        public InsertRestaurant(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Insert(NewRestaurant restaurant)
        {
            var result = await _unitOfWork.Connection.QueryAsync<long>(
                BaseQuery,
                new
                {
                    restaurant.Name,
                    restaurant.Description,
                    restaurant.City, 
                    restaurant.State
                });
            return result.Single();
        }
    }
}