

using System;
using System.Collections.Concurrent;

namespace RestaurantReviews.Api.Model
{
    public class Restaurant
    {
        public string Name { get; set; }

        public string StreetAddress { get; set; }
        public string City { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public Guid RestaurantId { get; set; }

        public Restaurant() { } 

        public Restaurant(string name, string streetAddress, string city, string region, string country, string postalCode)
        {
            Name = name;
            StreetAddress = streetAddress;
            City = city;
            Region = region;
            Country = country;
            PostalCode = postalCode;
        }

        public string GetKey()
        {
            return Name + ":" + StreetAddress + ":" + City + ":" + Region + ":" + Country;
        }
    }
}