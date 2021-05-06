using System.ComponentModel.DataAnnotations;

namespace RestaurantReviewsAPI.Models.DataTransferObjects
{
    public class RestaurantNewDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public long CityId { get; set; }
    }
}
