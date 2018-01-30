using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReview.Data.Entities
{
    public class RestaurantContext
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string streetAddress { get; set; }
        [Required]
        public string zipcode { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string state { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string thumbnailBase64 { get; set; }
    }

    public partial class RestaurantDBContext : DbContext
    {
        public RestaurantDBContext() : base("name=RestaurantDBContext")
        {

        }

        public DbSet<RestaurantContext> Restaurants { get; set; }
    }
}
