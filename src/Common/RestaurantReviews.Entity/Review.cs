using RestaurantReviews.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Entity
{
    public class Review : IEntity
    {
        [Key]
        [Range(0,0)]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Range(0,0)]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("Restaurant")]
        [Required]
        [Column("restaurant_id")]
        public int RestaurantId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Heading { get; set; }

        public string Content { get; set; }

        [Required]
        [Range(1,10)]
        public int Rating { get; set; }
    }
}
