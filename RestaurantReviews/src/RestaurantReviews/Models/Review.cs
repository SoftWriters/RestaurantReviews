using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Models
{
    public class Review
    {
        private decimal _score;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ReviewId { get; set; }
        public decimal Score {
            get
            {
                return _score;
            }
            set
            {
                _score = (value < 0) ? 0 : (value > 5) ? 5 : value;
            }
        }
        [Required]
        public long UserId { get; set; }
        public virtual User User {get;set;}
        [MaxLength(1000)]
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime RatingDateTime { get; set; }
        [Required]
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}