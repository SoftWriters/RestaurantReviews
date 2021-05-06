using System.ComponentModel.DataAnnotations;

namespace RestaurantReviewsAPI.Models.DataTransferObjects
{
    public class LoginDTO
    {
        [Required]
        [StringLength(20)]
        public string uid { get; set; }

        [Required]
        [StringLength(20)]
        public string pwd { get; set; }
    }
}
