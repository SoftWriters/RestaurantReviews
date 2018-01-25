using System;
using System.Collections.Generic;
using RestaurantReview.BL.Base;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReview.BL.Model
{
    public partial class Restaurant : ModelBase.Model<Restaurant>
    {
        public Restaurant()
        {
        }


        public int CityID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
