using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Models
{
    public class Contact
    {
        public long ContactId { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        public Address Address { get; set; }
        public long AddressId { get; set; }
    }
}