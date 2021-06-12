using RestaurantReviews.Core.Interfaces;
using System;

namespace RestaurantReviews.Core.DataTypes
{
    /// <summary>
    /// Basic In-memory implementation of IAddress
    /// </summary>
    public class Address : IAddress
    {
        public Address()
            : this(Guid.NewGuid())
        {

        }

        public Address(Guid uniqueId)
        {
            UniqueId = uniqueId;
        }

        public Address(IAddress other)
        {
            UniqueId = other.UniqueId;
            StreetLine1 = other.StreetLine1;
            StreetLine2 = other.StreetLine2;
            BuildingNumber = other.BuildingNumber;
            City = other.City;
            CountryOrRegion = other.CountryOrRegion;
            StateOrProvince = other.StateOrProvince;
            PostalCode = other.PostalCode;
        }

        public Guid UniqueId { get; }

        public string StreetLine1 { get; set; }

        public string StreetLine2 { get; set; }

        public string BuildingNumber { get; set; }

        public string City { get; set; }

        public string CountryOrRegion { get; set; }

        public string StateOrProvince { get; set; }

        public string PostalCode { get; set; }
    }
}
