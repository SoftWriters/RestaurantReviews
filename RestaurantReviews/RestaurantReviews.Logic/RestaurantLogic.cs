using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data;
using RestaurantReviews.Logic.Model.Restaurant.Post;
using RestaurantReviews.Logic.Model.Restaurant.Query;
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
        Task<PostRestaurantResponse> CreateRestaurant(PostRestaurantRequest request);
        Task<RestaurantQueryResponse> QueryRestaurant(RestaurantQueryRequest request);
        Task<ReviewQueryResponse> QueryReview(ReviewQueryRequest request);
        Task<UserQueryResponse> QueryUser(UserQueryRequest request);
    }

    public class RestaurantLogic : IRestaurantLogic
    {
        private readonly RestaurantContext context;

        public RestaurantLogic(RestaurantContext context)
        {
            this.context = context;
        }

        public async Task<PostRestaurantResponse> CreateRestaurant(PostRestaurantRequest request)
        {
            var entity = request.Build();
            await context.Restaurants.AddAsync(entity);
            await context.SaveChangesAsync();
            return new PostRestaurantResponse()
            {
                RestaurantId = entity.Id.ToString()
            };
        }

        public async Task<RestaurantQueryResponse> QueryRestaurant(RestaurantQueryRequest request)
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
                    };
                })
            };
        }

        public async Task<ReviewQueryResponse> QueryReview(ReviewQueryRequest request)
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

        public async Task<UserQueryResponse> QueryUser(UserQueryRequest request)
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
