using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviewsAPI.Models
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long RatingId { get; set; }
        public Rating Rating { get; set; }

        [Required]
        [Column(TypeName = "varchar(512)")]
        public string Comment { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDT { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public bool Deleted { get; set; } = false;
        public Nullable<DateTime> DeleteDT { get; set; }

        public long MobileUserID { get; set; }
        public MobileUser MobileUser { get; set; }

        public long RestaurantID { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
