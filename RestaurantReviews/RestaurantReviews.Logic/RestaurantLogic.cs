using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data;
using RestaurantReviews.Logic.Model.Review.Query;
using RestaurantReviews.Logic.Model.User.Query;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Logic
{
    public interface IRestaurantLogic
    {
        Task<UserQueryResponse> UserQuery(UserQueryRequest request);
        Task<ReviewQueryResponse> ReviewQuery(ReviewQueryRequest request);
    }

    public class RestaurantLogic : IRestaurantLogic
    {
        private readonly RestaurantContext context;

        public RestaurantLogic(RestaurantContext context)
        {
            this.context = context;
        }

        public async Task<ReviewQueryResponse> ReviewQuery(ReviewQueryRequest request)
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

        public async Task<UserQueryResponse> UserQuery(UserQueryRequest request)
        {
            IQueryable<User> query = context.Users;

            if (request.UserIds != null)
            {
                query = query.Where(p => request.UserIds.Contains(p.Id.ToString()));
            }

            var result = await query.ToListAsync();
            return new UserQueryResponse
            {
                Users = result.Select(p => new UserQueryResponseUser
                {
                    Id = p.Id.ToString(),
                    Name = p.ToString()
                })
            };
        }
    }
}
