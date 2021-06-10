namespace RestaurantReviews.Core
{
    public class Address : IAddress //Maybe drop the interface
    {
        public string StreetLine1 { get; set; }

        public string StreetLine2 { get; set; }

        public string BuildingNumber { get; set; }

        public string City { get; set; }

        public string CountryOrRegion { get; set; }

        public string StateOrProvince { get; set; }

        public string PostalCode { get; set; }
    }
}
