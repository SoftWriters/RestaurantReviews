using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data;
using RestaurantReviews.Model.Review.Query;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Logic
{
    public interface IRestaurantLogic
    {
        Task<ReviewQueryResponse> FindReviews(ReviewQueryRequest request);
    }

    public class RestaurantLogic : IRestaurantLogic
    {
        private readonly RestaurantContext context;

        public RestaurantLogic(RestaurantContext context)
        {
            this.context = context;
        }

        public async Task<ReviewQueryResponse> FindReviews(ReviewQueryRequest request)
        {
            IQueryable<Review> query = context.Reviews
                .Include(p => p.User);

            if (request.UserIds != null)
            {
                query = query.Where(p => request.UserIds.Contains(p.UserId));
            }

            var result = await query.ToListAsync();
            return new ReviewQueryResponse
            {
                Reviews = result.Select(p => new ReviewQueryResponseReview
                {
                    ReviewId = p.Id.ToString(),
                    ReviewText = p.ReviewText,
                    UserId = p.UserId.ToString(),
                    UserName = p.User.ToString()
                })
            };
        }
    }
}
