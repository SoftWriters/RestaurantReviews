using RestaurantReviewsApi.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Bll.Managers
{
    public interface IReviewManager
    {
        public Task<ReviewApiModel> GetReview(Guid reviewId);
        public Task<bool> PostReview(ReviewApiModel model);
        public Task<bool> DeleteReview(Guid reviewId);
        public Task<ICollection<ReviewApiModel>> SearchReviews(Guid? restaurantId, string username);
    }
}
