using RestaurantReviews.Interfaces.Models;

namespace RestaurantReviews.Models
{
    public class Review : IReview
    {
        public long Id { get; set; }
        public long RestaurantId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; }
    }
}
