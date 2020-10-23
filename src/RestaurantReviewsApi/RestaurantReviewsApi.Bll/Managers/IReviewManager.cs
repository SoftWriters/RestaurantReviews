using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Bll.Managers
{
    public interface IReviewManager
    {
        public Task<ReviewApiModel> GetReviewAsync(Guid reviewId);
        public Task<Guid?> PostReviewAsync(ReviewApiModel model, UserModel userModel);
        public Task<bool> DeleteReviewAsync(Guid reviewId, UserModel userModel);
        public Task<ICollection<ReviewApiModel>> SearchReviewsAsync(ReviewSearchApiModel model);
    }
}
