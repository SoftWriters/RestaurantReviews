using RestaurantReviews.Common;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Entity
{
    public class User : IEntity
    {
        [Key]
        [Range(0,0)]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string UserName { get; set; }
    }
}
