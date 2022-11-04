using RestaurantReviews.Classes;
using RestaurantReviews.Entities;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.WebServices.Models
{
    public class RestaurantModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public RestaurantLocation RestaurantLocation { get; set; }
    }
}