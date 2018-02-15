using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.Models;

namespace RestaurantReviews.Data.EfLibrary.Entities
{
    [Table("Reviews")]
    public class ReviewDBO
    {
        public long Id { get; set; }
        [Required]
        public RestaurantDBO Restaurant { get; set; }
        [Required]
        public virtual UserDBO Author { get; set; }
        public int Stars { get; set; }
        [MaxLength(Review.MaxCommentLength)]
        public string Comments { get; set; }
    }
}
