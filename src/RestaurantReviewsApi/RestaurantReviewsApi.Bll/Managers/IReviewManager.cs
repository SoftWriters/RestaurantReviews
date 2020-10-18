using RestaurantReviewsApi.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Bll.Managers
{
    public interface IReviewManager
    {
        public Task<ReviewApiModel> GetReviewAsync(Guid reviewId);
        public Task<bool> PostReviewAsync(ReviewApiModel model);
        public Task<bool> DeleteReviewAsync(Guid reviewId);
        public Task<ICollection<ReviewApiModel>> SearchReviewsAsync(ReviewSearchApiModel model);
    }
}
