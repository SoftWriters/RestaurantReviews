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

        public async Task<bool> DeleteReviewAsync(Guid reviewId)
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

        public async Task<ReviewApiModel> GetReviewAsync(Guid reviewId)
        {
            var review = await _dbContext.Review.FirstOrDefaultAsync(x =>
                x.ReviewId == reviewId &&
                !x.IsDeleted);

            if (review == null)
                return null;

            return _translator.ToReviewApiModel(review);
        }

        public async Task<Guid?> PostReviewAsync(ReviewApiModel model)
        {
            var review = _translator.ToReviewModel(model);
            _dbContext.Review.Add(review);
            await _dbContext.SaveChangesAsync();
            return review.ReviewId;
        }

        public async Task<ICollection<ReviewApiModel>> SearchReviewsAsync(ReviewSearchApiModel model)
        {
            var reviews = _dbContext.Review.Include(r => r.Restaurant).AsNoTracking().Where(x => !x.IsDeleted);

            if (model.UserName != null)
                reviews = reviews.Where(x => x.UserName == model.UserName);
            if (model.RestaurantId != null)
                reviews = reviews.Where(x => x.Restaurant.RestaurantId == model.RestaurantId);
           
            var reviewList = await reviews.ToListAsync();

            List<ReviewApiModel> returnList = new List<ReviewApiModel>();

            reviewList.ForEach(x =>
            {
                returnList.Add(_translator.ToReviewApiModel(x));
            });

            return returnList;
        }
    }
}
