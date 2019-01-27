using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api.DataAccess
{
    public class InsertReview : IInsertReview
    {
        private const string BaseQuery = @"
            INSERT INTO [dbo].[Review] (
                [RestaurantId]
                ,[ReviewerEmail]
                ,[RatingStars]
                ,[Comments]
                ,[ReviewedOn])
            VALUES (
                @RestaurantId,
                @ReviewerEmail,
                @RatingStars,
                @Comments,
                SYSDATETIMEOFFSET())
                
            SELECT CAST(SCOPE_IDENTITY() as int)";
        
        private readonly IUnitOfWork _unitOfWork;

        public InsertReview(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Insert(NewReview review)
        {
            var result = await _unitOfWork.Connection.QueryAsync<long>(
                BaseQuery,
                new
                {
                    review.RestaurantId,
                    review.ReviewerEmail,
                    review.RatingStars,
                    review.Comments
                });
            return result.Single();
        }
    }
}