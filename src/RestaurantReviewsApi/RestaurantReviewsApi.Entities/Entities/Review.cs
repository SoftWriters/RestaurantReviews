using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviewsApi.Entities
{
    public partial class Review
    {
        public Guid RestaurantId { get; set; }
        [Key]
        public Guid ReviewId { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        public int Rating { get; set; }
        [StringLength(4000)]
        public string Details { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty("Review")]
        public virtual Restaurant Restaurant { get; set; }
    }
}
