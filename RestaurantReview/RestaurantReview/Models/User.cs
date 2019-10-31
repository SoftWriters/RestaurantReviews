using System.ComponentModel.DataAnnotations;

namespace RestaurantReview.Models
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}