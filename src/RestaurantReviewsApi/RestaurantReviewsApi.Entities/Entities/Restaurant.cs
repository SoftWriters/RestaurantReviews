using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviewsApi.Entities
{
    public partial class Restaurant
    {
        public int SystemId { get; set; }
        [Key]
        public Guid RestaurantId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string AddressLine1 { get; set; }
        [StringLength(100)]
        public string AddressLine2 { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(2)]
        public string State { get; set; }
        [StringLength(10)]
        public string ZipCode { get; set; }
        [StringLength(12)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Website { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
    }
}
