using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Data.Models
{
    public class Restaurant
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string StateCode { get; set; }
    }
}
