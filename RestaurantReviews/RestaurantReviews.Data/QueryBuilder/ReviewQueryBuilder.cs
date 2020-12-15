using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Logic.Model.Review.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Data.QueryBuilder
{
    public interface IReviewQueryBuilder : IQueryBuilderSearch<Review, SearchReviewRequest, SearchReview>
    { }

    public class ReviewQueryBuilder : IReviewQueryBuilder
    {
        public SearchReview BuildSearchEntity(Review entity)
        {
            return new SearchReview
            {
                ReviewId = entity.Id.ToString(),
                ReviewText = entity.ReviewText,
                UserId = entity.UserId.ToString(),
                UserName = entity.User.ToString()
            };
        }

        public IQueryable<Review> BuildSearchQuery(IQueryable<Review> dbSet, SearchReviewRequest request)
        {
            IQueryable<Review> query = dbSet.Include(p => p.User);

            if (request?.UserIds != null)
            {
                query = query.Where(p => request.UserIds.Contains(p.UserId.ToString()));
            }

            return query
                .OrderByDescending(p => p.DateCreated);
        }
    }
}
