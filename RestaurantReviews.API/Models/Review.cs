using RestaurantReviews.Interfaces.Models;

namespace RestaurantReviews.API.Models
{
    public class Review : IReview
    {
        public long Id { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
