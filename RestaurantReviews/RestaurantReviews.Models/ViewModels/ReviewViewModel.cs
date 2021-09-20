using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Models
{
    public class ReviewViewModel
    {
        public int UserID { get; set; }
        public int RestaurantID { get; set; }
        public List<ReviewInfoModel> Reviews { get; set; }
        public List<UserModel> Users { get; set; }
        public List<RestaurantInfoModel> Restaurants { get; set; }
    }
}
