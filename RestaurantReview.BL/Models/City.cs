using System;
using System.Collections.Generic;
using RestaurantReview.BL.Base;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReview.BL.Model
{
    public partial class City : ModelBase.Model<City>
    {
        public City()
        {
        }

        [Required]
        [StringLength(50)]
        public string CityName { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }
    }
}
