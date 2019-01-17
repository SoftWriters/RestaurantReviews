using RestaurantReviews.Common;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Entity
{
    public class Restaurant : IEntity
    {
        [Key]
        [Range(0,0)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }
    }
}
