using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantRating.Models
{

    [Table("tblUsers")]
    public class Users
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Name Of Restaurant")]
        [Required]
        [MaxLength(40, ErrorMessage = "RestaurantName cannot be longer than 50 characters.")]
        public string NameOfRest { get; set; }

        [Required]
        [DisplayName("User")]
        [MaxLength(40, ErrorMessage = "Name cannot be longer than 40 characters.")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Use letters only please")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Review cannot be longer than 250 characters.")]
        [DisplayName("Reviews")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Use letters only please")]
        public string Reviews { get; set; }

        //One user can post review only for many restaurants
        public List<Restaurant> restaurant { get; set; }
    }
}