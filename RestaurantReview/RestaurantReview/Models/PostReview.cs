using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RestaurantReview.Models
{
    public class PostReview
    {
        
        public int? ReviewId { get; set; }

        [Required]
        public int? RestaurantId { get; set; }

        [Required]
        public int UserId{ get; set; }

        [Required]
        public string ReviewText { get; set; }
        
        public bool IsValidId()
        {
            return this.RestaurantId > 0;
        }

        public bool IsValidReviewText()
        {
            return this.ReviewText.Split(" ").Length > 1;
        }

    }
}