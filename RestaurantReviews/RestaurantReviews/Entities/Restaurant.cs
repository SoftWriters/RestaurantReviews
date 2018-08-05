using RestaurantReviews.Classes;
using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public RestaurantLocation RestaurantLocation { get; set; }
    }
}