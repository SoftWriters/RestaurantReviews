using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantReviews.DataAccess;
using RestaurantReviews.Models;
using RestaurantReviews.Repositories.Interfaces;

namespace RestaurantReviews.Services.Repositories
{
    /// <summary>
    /// Repository object for reviews. 
    /// </summary>
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(RestaurantReviewContext context) : base(context)
        {
        }

        public IEnumerable<Review> GetReviewsByUser(UserRequest user)
        {
            if (user == null) return null;
            return context.Review.Where(review =>
                (review.UserId == user.UserId && user.UserId != 0) || 
                (review.User.UserName == user.UserName && !string.IsNullOrEmpty(user.UserName)) ||
                (review.User.ContactInformation.Email == user.Email && !string.IsNullOrEmpty(user.Email))
                )
                .ToList();
        }

        public Review GetReview(long reviewId)
        {
            return context.Review.Where(review => review.ReviewId == reviewId).FirstOrDefault();
        }

        public IEnumerable<Review> GetReviewsByRestaurant(RestaurantRequest restaurant)
        {
            if (restaurant == null) return null;
            return context.Review.Where(review => 
                (review.RestaurantId == restaurant.RestaurantId && restaurant.RestaurantId != 0)
            ).ToList();
        }

        public void Add(ReviewRequest review)
        {
            context.Review.Add(new Review
            {
                UserId = review.UserId,
                RestaurantId = review.RestaurantId,
                Comment = review.Comment,
                Score = review.Score,
                RatingDateTime = review.RatingDateTime
            });
        }

        public void Delete(long reviewId)
        {
            var reviewToDelete = context.Review.Where(review => review.ReviewId == reviewId).FirstOrDefault();
            if (reviewToDelete != null)
            {
                context.Review.Attach(reviewToDelete);
                context.Review.Remove(reviewToDelete);
            }
        }
    }
}