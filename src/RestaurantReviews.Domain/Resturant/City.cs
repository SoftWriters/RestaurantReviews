using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Domain
{
    public class City
    {
        public Guid CityId { get; }
        public string Name { get; }
        public string StateOrProvince { get; }
        public string Country { get; }


        public City(Guid cityId, string name, string stateOrProvince, string country)
        {
            this.CityId = cityId;
            this.Name = name;
            this.StateOrProvince = stateOrProvince;
            this.Country = country;
        }
    }
}
