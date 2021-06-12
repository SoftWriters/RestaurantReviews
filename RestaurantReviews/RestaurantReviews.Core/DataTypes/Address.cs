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

        //public Address(Guid uniqueId = default(Guid), string streetLine1 = null, string streetLine2 = null, string buildingNumber = null, string city = null, string countryOrRegion = null, string stateOrProvince = null, string postalCode = null)
        //{
        //    UniqueId = (uniqueId == default(Guid)) ? Guid.NewGuid() : uniqueId;
        //    StreetLine1 = streetLine1;
        //    StreetLine2 = streetLine2;
        //    BuildingNumber = buildingNumber;
        //    City = city;
        //    CountryOrRegion = countryOrRegion;
        //    StateOrProvince = stateOrProvince;
        //    PostalCode = postalCode;
        //}

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
