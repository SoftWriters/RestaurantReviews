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
            var result = await request.BuildQuery(context.Reviews)
                .ToListAsync();
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
            var result = await request.BuildQuery(context.Users)
                .ToListAsync();
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
