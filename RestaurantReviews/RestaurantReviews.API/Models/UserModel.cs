using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.WebServices.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }
    }
}