using RestaurantReviewsApi.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Bll.Managers
{
    public class ReviewManager : IReviewManager
    {
        public Task<bool> DeleteReview(Guid reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewApiModel> GetReview(Guid reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostReview(ReviewApiModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ReviewApiModel>> SearchReviews(Guid? restaurantId, string username)
        {
            throw new NotImplementedException();
        }
    }
}
