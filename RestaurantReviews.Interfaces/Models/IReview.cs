namespace RestaurantReviews.Interfaces.Models
{
    public interface IReview : IModel
    {
        string Content { get; set; }
        int RestaurantId { get; set; }
        int UserId { get; set; }
    }
}