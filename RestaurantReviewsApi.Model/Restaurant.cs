using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Model
{
    public class Restaurant : IEntityBase
    {
        [Key]
        // In a more robust app, we'd generate new GUIDs as we add restaurants
        // public Guid Id { get => _id; set => _id = Guid.NewGuid(); }
        // 
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Url { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Price { get; set; }
        [DefaultValue(0)]
        public decimal Rating { get; set; }
    }
}