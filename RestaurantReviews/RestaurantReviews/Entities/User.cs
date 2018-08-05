using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantReviews.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Index(IsUnique=true)]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

    }
}