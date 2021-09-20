using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Models
{
    public class ReviewUpdateViewModel
    {
        public ReviewInfoModel Review { get; set; }
        public List<RestaurantInfoModel> Restaurants { get; set; }
    }
}
