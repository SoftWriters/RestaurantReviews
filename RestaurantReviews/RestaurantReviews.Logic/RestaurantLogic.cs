using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data;
using RestaurantReviews.Logic.Model.Restaurant;
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
        Task<RestaurantQueryResponse> RestaurantQuery(RestaurantQueryRequest request);
        Task<ReviewQueryResponse> ReviewQuery(ReviewQueryRequest request);
        Task<UserQueryResponse> UserQuery(UserQueryRequest request);
    }

    public class RestaurantLogic : IRestaurantLogic
    {
        private readonly RestaurantContext context;

        public RestaurantLogic(RestaurantContext context)
        {
            this.context = context;
        }

        public async Task<RestaurantQueryResponse> RestaurantQuery(RestaurantQueryRequest request)
        {
            var result = await request.BuildQuery(context.Restaurants)
                .ToListAsync();
            return new RestaurantQueryResponse()
            {
                Restaurants = result.Select(p =>
                {
                    return new RestaurantQueryResponseRestaurant()
                    {
                        Id = p.Id.ToString(),
                        Name = p.Name,
                        City = p.City,
                        State = p.State,
                        Zip = p.ZipCode
                    }
                })
            }
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
