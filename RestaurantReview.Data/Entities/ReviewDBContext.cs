
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReview.Data.Entities
{
    public class ReviewContext
    {
        public int id { get; set; }
        [Required]
        public int restaurantID { get; set; }
        [Required]
        public decimal rating { get; set; }
        public string comments { get; set; }
        [Required]
        public string userName { get; set; }
    }

    public partial class ReviewDBContext : DbContext
    {
        public ReviewDBContext() : base("name=ReviewDBContext")
        {

        }

        public DbSet<ReviewContext> Reviews { get; set; }
    }
}
