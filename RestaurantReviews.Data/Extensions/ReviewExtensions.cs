using RestaurantReviews.Data.Entities;

namespace RestaurantReviews.Data.Extensions
{
    public static class ReviewExtensions
    {
        public static void Map(this Review dbReview, Review review)
        {
            dbReview.Comment = review.Comment;
            dbReview.Rating = review.Rating;
            dbReview.RestaurauntId = review.RestaurauntId;
            dbReview.SubmissionDate = review.SubmissionDate;
            dbReview.UserId = review.UserId;
        }
    }
}
