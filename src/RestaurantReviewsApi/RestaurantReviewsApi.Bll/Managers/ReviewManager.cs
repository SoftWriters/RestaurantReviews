using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Translators;
using RestaurantReviewsApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Bll.Managers
{
    public class ReviewManager : IReviewManager
    {
        private readonly ILogger<ReviewManager> _logger;
        private readonly RestaurantReviewsContext _dbContext;
        private readonly IApiModelTranslator _translator;

        public ReviewManager(ILogger<ReviewManager> logger, RestaurantReviewsContext dbContext, IApiModelTranslator translator)
        {
            _logger = logger;
            _dbContext = dbContext;
            _translator = translator;
        }

        public async Task<bool> DeleteReview(Guid reviewId)
        {
            var review = await _dbContext.Review.FirstOrDefaultAsync(x =>
                x.ReviewId == reviewId &&
                !x.IsDeleted);

            if (review == null)
                return false;

            review.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ReviewApiModel> GetReview(Guid reviewId)
        {
            var review = await _dbContext.Review.FirstOrDefaultAsync(x =>
                x.ReviewId == reviewId &&
                !x.IsDeleted);

            return _translator.ToReviewApiModel(review);
        }

        public async Task<bool> PostReview(ReviewApiModel model)
        {
            var review = _translator.ToReviewModel(model);
            _dbContext.Review.Add(review);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async IAsyncEnumerable<ReviewApiModel> SearchReviews(ReviewSearchApiModel model)
        {
            var reviews = _dbContext.Review.AsNoTracking().Where(x => !x.IsDeleted);

            if (model.UserName != null)
                reviews = reviews.Where(x => x.UserName == model.UserName);
            if (model.RestaurantId != null)
                reviews = reviews.Where(x => x.RestaurantId == model.RestaurantId);
           
            var ret = await reviews.ToListAsync();

            foreach (var r in ret)
            {
                yield return _translator.ToReviewApiModel(r);
            }
        }
    }
}
