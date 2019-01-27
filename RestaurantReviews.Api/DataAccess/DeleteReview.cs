using System.Threading.Tasks;
using Dapper;

namespace RestaurantReviews.Api.DataAccess
{
    public class DeleteReview : IDeleteReview
    {
        private const string BaseQuery = "DELETE FROM [dbo].[Review] WHERE Id=@Id";
        
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReview(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> Delete(long id)
        {
            var rowCount = _unitOfWork.Connection.ExecuteAsync(BaseQuery, new {Id = id});
            return rowCount;
        }
    }
}