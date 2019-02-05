using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.API.DTOs
{
    public class ResturantDTO
    {
        public Guid ResturantId { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public string FullAddress { get; set; }

    }
}
