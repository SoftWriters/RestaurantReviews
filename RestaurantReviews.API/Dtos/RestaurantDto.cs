using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.API.Dtos
{
    public class RestaurantDto
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address can't be longer than 100 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City can't be longer than 100 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(50, ErrorMessage = "City can't be longer than 50 characters")]
        public string Country { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(2, ErrorMessage = "State can't be longer than 2 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "PostalCode is required")]
        [StringLength(11, ErrorMessage = "State can't be longer than 11 characters")]
        public string PostalCode { get; set; }

        [StringLength(30, ErrorMessage = "Phone can't be longer than 30 characters")]
        public string Phone { get; set; }

        [StringLength(200, ErrorMessage = "Website Url can't be longer than 200 characters")]
        public string WebsiteUrl { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [StringLength(60, ErrorMessage = "Email Address can't be longer than 60 characters")]
        public string EmailAddress { get; set; }
    }
}
