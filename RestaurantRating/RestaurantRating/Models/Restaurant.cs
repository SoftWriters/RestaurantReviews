using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace RestaurantRating.Models
{
    [Table("tblRestaurant")]
    public class Restaurant
    {

        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        // public int RestaurantId { get; set; }
        [Key]
        [MaxLength(50, ErrorMessage = "Restaurant Name cannot be longer than 50 characters.")]
        [Required]
        [DisplayName("RestaurantName")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Use letters only please")]
        public string RestaurantName { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "City Name cannot be longer than 40 characters.")]
        [DisplayName("City")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Use letters only please")]
        public string City { get; set; }
        //One restaurant can have multiple users posting their reviews
        public List<Users> user { get; set; }

    }
}