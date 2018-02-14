using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.Models
{
    public class Review
    {
        public long Id { get; set; }
        [Required]
        public int Stars { get; set; }
        public const int MaxCommentLength = 1000;
        [MaxLength(MaxCommentLength)]
        public string Comments { get; set; }
    }
}
