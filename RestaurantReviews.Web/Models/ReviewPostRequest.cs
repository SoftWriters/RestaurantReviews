using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Web.Models
{
    public class ReviewPostRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
