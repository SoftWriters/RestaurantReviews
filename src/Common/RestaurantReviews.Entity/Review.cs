using RestaurantReviews.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entity
{
    public class Review : IEntity
    {
        [Range(0,0)]
        public int Id { get; set; }
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Key]
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
