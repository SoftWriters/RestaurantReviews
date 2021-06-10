namespace RestaurantReviews.Core
{
    //Using guidelines from https://docs.microsoft.com/en-us/globalization/locale/addresses, loosely based on CivicAddress class
    public interface IAddress //TODO: maybe just use a class for this
    {
        string StreetLine1 { get; }
        string StreetLine2 { get; }
        string BuildingNumber { get; }
        string City { get; }
        string CountryOrRegion { get; }
        string StateOrProvince { get; }
        string PostalCode { get; }
    }
}
