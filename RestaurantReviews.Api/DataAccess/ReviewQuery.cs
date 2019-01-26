using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api.DataAccess
{
    public class ReviewQuery : IReviewQuery
    {
        private const string BaseQuery = "SELECT * FROM dbo.Review";
        
        private readonly IUnitOfWork _unitOfWork;

        public ReviewQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Review> GetReview(long id)
        {
            var result = await _unitOfWork.Connection.QueryAsync<Review>(
                BaseQuery + " WHERE Id=@Id",
                new { Id = id });
            return result.FirstOrDefault();
        }
    }
}