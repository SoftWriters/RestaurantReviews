using RestaurantReviews.Logic.Model;
using RestaurantReviews.Logic.Model.Restaurant.Create;
using RestaurantReviews.Logic.Model.Restaurant.Search;
using RestaurantReviews.Logic.Model.Review.Search;
using RestaurantReviews.Logic.Model.User.Search;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Logic
{
    public interface IRestaurantLogic
    {
        Task<CreateResponse> CreateRestaurant(CreateRestaurantRequest request);
        Task<SearchResponse<SearchRestaurant>> SearchRestaurants(SearchRestaurantRequest request);
        Task<SearchResponse<SearchReview>> SearchReviews(SearchReviewRequest request);
        Task<SearchResponse<SearchUser>> SearchUsers(SearchUserRequest request);
    }
}
