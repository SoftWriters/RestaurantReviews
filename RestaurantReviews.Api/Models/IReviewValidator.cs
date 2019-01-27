namespace RestaurantReviews.Api.Models
{
    public interface IReviewValidator
    {
        bool IsReviewValid(NewReview review);
    }
}