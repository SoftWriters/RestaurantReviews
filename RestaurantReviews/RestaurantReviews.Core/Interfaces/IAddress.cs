using System;

namespace RestaurantReviews.Core.Interfaces
{
    /// <summary>
    /// Representation of an international address. All fields are not required.
    /// Format and validation is dependent on the region.
    /// </summary>
    /// <remarks>
    /// Using guidelines from https://docs.microsoft.com/en-us/globalization/locale/addresses, loosely based on CivicAddress class
    /// </remarks>
    public interface IAddress
    {
        /// <summary>
        /// Unique identifier for the address so that it can be reused by multiple instances,
        /// e.g. multiple restaurants in the same building, or different restaurants at the same address over time.
        /// </summary>
        Guid UniqueId { get; }

        string StreetLine1 { get; }

        string StreetLine2 { get; }

        string BuildingNumber { get; }

        string City { get; }

        string CountryOrRegion { get; }

        /// <summary>
        /// State or province. Could be an abbreviation or the full name
        /// </summary>
        string StateOrProvince { get; }

        /// <summary>
        /// Postal code. Format is dependent on the country, maximum of 10 characters.
        /// </summary>
        string PostalCode { get; }
    }
}
