using System.ComponentModel.DataAnnotations;

namespace RestaurantReview.Models
{
    public class UpdateReview
    {
        [Required]
        public int ReviewId { get; set; }
        [Required]
        public string ReviewText { get; set; }
    }
}