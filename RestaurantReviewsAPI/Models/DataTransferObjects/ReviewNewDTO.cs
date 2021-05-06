using System.ComponentModel.DataAnnotations;


namespace RestaurantReviewsAPI.Models.DataTransferObjects
{
    public class ReviewNewDTO
    {
        public long RatingId { get; set; }

        [StringLength(512)]
        public string Comment { get; set; }

        public long UserId { get; set; }

        public long RestaurantId { get; set; }
    }
}
