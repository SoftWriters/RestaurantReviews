namespace RestaurantReviews.Interfaces.Models
{
    public interface IReview : IModel
    {
        string Content { get; set; }
        long RestaurantId { get; set; }
        long UserId { get; set; }
    }
}