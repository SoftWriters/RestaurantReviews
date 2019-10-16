using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReview.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public Restaurant Restaurant { get; set; }
        public string User { get; set; }
    }
}
