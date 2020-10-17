using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviewsApi.Entities.Entities
{
    public partial class Review
    {
        public int SystemId { get; set; }
        [Key]
        public Guid ReviewId { get; set; }
        public Guid RestaurantId { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        public int Rating { get; set; }
        [StringLength(4000)]
        public string Details { get; set; }
    }
}
