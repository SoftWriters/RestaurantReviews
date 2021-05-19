using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestarauntReviews.DTO
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int RestaurantId { get; set; }
        public string UserName { get; set; }
        public string ReviewDescription { get; set; }
        public int Score { get; set; }
    }
}
