using RestaurantReviews.Common;
using RestaurantReviews.Data;
using RestaurantReviews.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Domain
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsAsync(int page, int pagesize, DbFilter<Review> filter);
        Task<Review> GetReviewAsync(int id);
        Task<Review> CreateReviewAsync(int restaurantId, string heading, string content, int rating);
        Task DeleteReviewAsync(int id);
    }
    public class ReviewRepository : IReviewRepository
    {
        private IReviewDataManager _restaurantDataManager;
        private IUserInfoProvider _userInfoProvider;

        public ReviewRepository(IReviewDataManager restaurantDataManager, IUserInfoProvider userInfoProvider)
        {
            _restaurantDataManager = restaurantDataManager;
            _userInfoProvider = userInfoProvider;
        }

        public Task<Review> CreateReviewAsync(int restaurantId, string heading, string content, int rating)
        {
            var review = new Review() { UserId = _userInfoProvider.GetCurrentUserInfo().Id, RestaurantId = restaurantId, Heading = heading, Content = content, Rating = rating };
            return _restaurantDataManager.CreateReviewAsync(review);
        }

        public Task DeleteReviewAsync(int id)
        {
            return _restaurantDataManager.DeleteReviewAsync(id, _userInfoProvider.GetCurrentUserInfo().Id);
        }

        public Task<Review> GetReviewAsync(int id)
        {
            return _restaurantDataManager.GetReviewAsync(id);
        }

        public Task<IEnumerable<Review>> GetReviewsAsync(int page, int pagesize, DbFilter<Review> filter)
        {
            return _restaurantDataManager.GetReviewsAsync(page, pagesize, filter);
        }
    }
}
