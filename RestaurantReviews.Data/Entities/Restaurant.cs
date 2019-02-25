using RestaurantReviews.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Data.Entities
{
    [Table("Restaurants", Schema = "DomainData")]
    public class Restaurant : IEntity
    {
        [Key]
        [Column("RestaurantId")]
        public Guid Id { get; set; }

        [Column("Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; }

        [Column("Address")]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address can't be longer than 100 characters")]
        public string Address { get; set; }

        [Column("City")]
        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City can't be longer than 100 characters")]
        public string City { get; set; }

        [Column("Country")]
        [Required(ErrorMessage = "Country is required")]
        [StringLength(50, ErrorMessage = "City can't be longer than 50 characters")]
        public string Country { get; set; }

        [Column("State")]
        [Required(ErrorMessage = "State is required")]
        [StringLength(20, ErrorMessage = "State can't be longer than 20 characters")]
        public string State { get; set; }

        [Column("PostalCode")]
        [Required(ErrorMessage = "PostalCode is required")]
        [StringLength(11, ErrorMessage = "State can't be longer than 11 characters")]
        public string PostalCode { get; set; }

        [Column("Phone")]
        [StringLength(30, ErrorMessage = "Phone can't be longer than 30 characters")]
        public string Phone { get; set; }

        [Column("WebsiteUrl")]
        [StringLength(200, ErrorMessage = "Website Url can't be longer than 200 characters")]
        public string WebsiteUrl { get; set; }

        [Column("EmailAddress")]
        [Required, DataType(DataType.EmailAddress)]
        [StringLength(60, ErrorMessage = "Email Address can't be longer than 60 characters")]
        public string EmailAddress { get; set; }

        [Column("IsConfirmed")]
        public bool IsConfirmed { get; set; } = false;
    }
}
