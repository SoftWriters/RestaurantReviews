using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviewsAPI.Models
{
    public class Rating
    {
        public long Id { get; set; }

        public long Value { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
    }
}
