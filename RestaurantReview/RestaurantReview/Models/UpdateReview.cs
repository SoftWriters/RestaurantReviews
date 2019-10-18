using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReview.Models
{
    public class UpdateReview
    {
        public int ReviewId { get; set; }
        public string ReviewText { get; set; }
    }
}
