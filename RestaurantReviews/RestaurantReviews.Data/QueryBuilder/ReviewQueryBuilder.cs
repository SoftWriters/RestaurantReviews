using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Logic.Model.Review.Create;
using RestaurantReviews.Logic.Model.Review.Delete;
using RestaurantReviews.Logic.Model.Review.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Data.QueryBuilder
{
    public interface IReviewQueryBuilder : 
        IQueryBuilderSearch<Review, SearchReviewRequest, SearchReview>,
        IQueryBuilderUpsert<Review, CreateReviewRequest>,
        IQueryBuilderSingle<Review, DeleteReviewRequest>
    { }

    public class ReviewQueryBuilder : IReviewQueryBuilder
    {
        public SearchReview BuildSearchResponse(Review entity)
        {
            return new SearchReview
            {
                ReviewId = entity.Id.ToString(),
                ReviewText = entity.ReviewText,
                RestaurantId = entity.RestaurantId.ToString(),
                RestaurantName = entity.Restaurant.Name,
                UserId = entity.UserId.ToString(),
                UserName = entity.User.ToString()
            };
        }

        public IQueryable<Review> BuildSearchQuery(IQueryable<Review> dbSet, SearchReviewRequest request)
        {
            IQueryable<Review> query = dbSet
                .Include(p => p.User)
                .Include(p => p.Restaurant);

            if (request?.UserIds != null)
            {
                query = query.Where(p => request.UserIds.Contains(p.UserId.ToString()));
            }

            return query
                .OrderByDescending(p => p.DateCreated);
        }

        public Review BuildEntityUpsert(CreateReviewRequest request)
        {
            return new Review()
            {
                Id = Guid.NewGuid(),
                RestaurantId = Guid.Parse(request.RestaurantId),
                UserId = Guid.Parse(request.UserId),
                ReviewText = request.ReviewText
            };
        }

        public IQueryable<Review> BuildQuerySingle(IQueryable<Review> dbSet, CreateReviewRequest request)
        {
            return dbSet.Where(p => 
                p.UserId.ToString() == request.UserId && 
                p.RestaurantId.ToString() == request.RestaurantId);
        }

        public IQueryable<Review> BuildQuerySingle(IQueryable<Review> dbSet, DeleteReviewRequest request)
        {
            return dbSet.Where(p => p.Id.ToString() == request.ReviewId);
        }
    }
}
