using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviewsAPI.Models
{
    public class City
    {
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "char(2)")]
        public string State { get; set; }
       
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
