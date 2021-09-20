using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Models
{
    public class ReviewInfoModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RestaurantID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserDisplayName { get; set; }
        public string RestaurantName { get; set; }
    }
}
