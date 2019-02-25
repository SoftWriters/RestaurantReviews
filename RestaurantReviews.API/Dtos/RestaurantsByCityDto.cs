using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.API.Dtos
{
    public class RestaurantsByCityDto
    {
        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City can't be longer than 100 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(2, ErrorMessage = "State can't be longer than 2 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string Country { get; set; }
    }
}
