using Dapper;
using RestaurantReviews.Common;
using RestaurantReviews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Data
{
    public interface IReviewDataManager
    {
        Task<Review> CreateReviewAsync(Review review);
        Task DeleteReviewAsync(int id, int userId);
        Task<Review> GetReviewAsync(int id);
        Task<IEnumerable<Review>> GetReviewsAsync(int page, int pagesize, DbFilter<Review> filter);
    }
    public class ReviewDataManager : DataManagerBase, IReviewDataManager
    {
        public ReviewDataManager(IDbContext context) : base(context) { }

        public async Task<IEnumerable<Review>> GetReviewsAsync(int page, int pagesize, DbFilter<Review> filter)
        {
            using (var db = GetConnection())
            {
                var query = @"SELECT  id, user_id as UserId, restaurant_id as RestaurantId, heading, content, rating
                                FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY heading ) AS RowNum, id, user_id, restaurant_id, heading, content, rating
                                          FROM      Review
                                          <<whereclause>>
                                        ) AS RowConstrainedResult
                                WHERE   RowNum >= @start
                                    AND RowNum < @end
                                ORDER BY RowNum";
                IEnumerable<Review> result;
                var start = ((page - 1) * pagesize) + 1;
                var end = start + pagesize;
                object param;
                if (filter != null)
                {
                    query = query.Replace("<<whereclause>>", string.Format("where {0}", filter.GetFilterSql("filterparam")));
                    param = new { start, end, filterparam = filter.Value };
                }
                else
                {
                    query = query.Replace("<<whereclause>>", "");
                    param = new { start, end };
                }
                result = await db.QueryAsync<Review>(query, param);
                return result;
            }
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            using (var db = GetConnection())
            {
                string insertQuery = @"INSERT INTO [dbo].[Review](user_id, restaurant_id, heading, content, rating) 
                        VALUES (@UserId, @RestaurantId, @Heading, @Content, @Rating); 
                    select @@IDENTITY as Id, @UserId as UserId, @RestaurantId as RestaurantId, @Heading as Heading, @Content as Content, @Rating as Rating";

                var result = await db.QueryFirstOrDefaultAsync<Review>(insertQuery, new
                {
                    review.UserId,
                    review.RestaurantId,
                    review.Heading,
                    review.Content,
                    review.Rating
                });
                return result;
            }
        }

        public async Task DeleteReviewAsync(int id, int userId)
        {
            using (var db = GetConnection())
            {
                string query = @"DELETE from [dbo].[Review] where id = @id and user_id = @userId";

                var result = await db.QueryAsync<Review>(query, new { id, userId});
            }
        }

        public async Task<Review> GetReviewAsync(int id)
        {
            using (var db = GetConnection())
            {
                var query = @"Select id, user_id as UserId, restaurant_id as RestaurantId, heading, content, rating from Review where Id = @id";

                var result = await db.QueryFirstOrDefaultAsync<Review>(query, new { id });
                return result;
            }
        }
    }
}
