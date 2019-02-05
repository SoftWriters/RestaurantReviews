using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.API.DTOs
{
    public class CityDTO
    {
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public string StateOrProvince { get; set; }
        public string Country { get; set; }
    }
}
