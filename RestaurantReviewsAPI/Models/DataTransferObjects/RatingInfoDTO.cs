using System.ComponentModel.DataAnnotations;

namespace RestaurantReviewsAPI.Models.DataTransferObjects
{
    public class RatingInfoDTO
    {
        public long RatingId { get; set; }
        public long Value { get; set; }
        public string Name { get; set; }
    }
}
