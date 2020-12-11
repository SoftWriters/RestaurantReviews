using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantReviews.Data
{
    public class Review : Entity
    {
        [StringLength(500)]
        public string ReviewText { get; set; }

        public User User { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}