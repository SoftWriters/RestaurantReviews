using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entities.Logic
{
    /// <summary>
    /// Exposes business logic for the RestaurantAddress class
    /// </summary>
    public static class RestaurantAddressManager
    {
        /// <summary>
        /// Validates that a member instance has the necessary information before persisting.  If string properties are composed of purely whitespace they will be set to null.
        /// </summary>
        /// <param name="address">A RestaurantAddress instance to validate.</param>
        private static void ValidateRestaurantAddress(RestaurantAddress address)
        {
            if (address.RestaurantId == -1)
                throw (new System.ArgumentException("Address restaurant id is invalid."));

            if (string.IsNullOrWhiteSpace(address.City))
                throw (new System.ArgumentException("City cannot be null or whitespace."));
            else if (address.City.Length > 100)
                throw (new System.ArgumentException("City exceeds maximum length of 100 characters."));

            if (string.IsNullOrWhiteSpace(address.Region))
                throw (new System.ArgumentException("Region cannot be null or whitespace."));
            else if (address.Region.Length > 100)
                throw (new System.ArgumentException("Region exceeds maximum length of 100 characters."));

            if (string.IsNullOrWhiteSpace(address.Street1))
                address.Street1 = null;
            else if (address.Street1.Length > 100)
                throw (new System.ArgumentException("Street1 exceeds maximum length of 100 characters."));

            if (string.IsNullOrWhiteSpace(address.Street2))
                address.Street2 = null;
            else if (address.Street2.Length > 100)
                throw (new System.ArgumentException("Street2 exceeds maximum length of 100 characters."));

            if (string.IsNullOrWhiteSpace(address.PostalCode))
                address.PostalCode = null;
            else if (address.PostalCode.Length > 10)
                throw (new System.ArgumentException("Postal code exceeds maximum length of 10 characters."));
        }
        /// <summary>
        /// Persists a new RestaurantAddress instance.
        /// </summary>
        /// <param name="restaurantId">The associated Restaurant ID</param>
        /// <param name="street1">The primary street address.</param>
        /// <param name="street2">The secondary street address.</param>
        /// <param name="city">The city associated with the address.</param>
        /// <param name="region">The region associated with the address.</param>
        /// <param name="postalcode">The postal code associated with the address.</param>
        /// <returns>The persisted instance of the address.</returns>
        public static RestaurantAddress CreateRestaurantAddress(long restaurantId, string street1, string street2, string city, string region, string postalcode)
        {
            RestaurantAddress address = new RestaurantAddress();
            address.RestaurantId = restaurantId;
            address.Street1 = street1;
            address.Street2 = street2;
            address.City = city;
            address.Region = region;
            address.PostalCode = postalcode;

            CreateRestaurantAddress(address);

            return address;
        }
        /// <summary>
        /// Persists a new RestaurantAddress instance.
        /// </summary>
        /// <param name="address">The address instance to persist.</param>
        public static void CreateRestaurantAddress(RestaurantAddress address)
        {
            if (address.Id != -1)
                throw (new System.ArgumentException("Address is not a new instance."));

            ValidateRestaurantAddress(address);

            Data.RestaurantAddressSQL.CreateRestaurantAddress(address);
        }
        /// <summary>
        /// Updates a RestaurantAddress instance.
        /// </summary>
        /// <param name="restaurantAddressId">The id of the address instance.</param>
        /// <param name="restaurantId">The associated Restaurant ID</param>
        /// <param name="street1">The primary street address.</param>
        /// <param name="street2">The secondary street address.</param>
        /// <param name="city">The city associated with the address.</param>
        /// <param name="region">The region associated with the address.</param>
        /// <param name="postalcode">The postal code associated with the address.</param>
        /// <returns></returns>
        public static RestaurantAddress UpdateRestaurantAddress(long restaurantId, long restaurantAddressId, string street1, string street2, string city, string region, string postalcode)
        {
            RestaurantAddress address = new RestaurantAddress();
            address.Id = restaurantAddressId;
            address.RestaurantId = restaurantId;
            address.Street1 = street1;
            address.Street2 = street2;
            address.City = city;
            address.Region = region;
            address.PostalCode = postalcode;

            UpdateRestaurantAddress(address);

            return address;
        }
        /// <summary>
        /// Updates a RestaurantAddress instance.
        /// </summary>
        /// <param name="address">The address instance to perform the update on.</param>
        public static void UpdateRestaurantAddress(RestaurantAddress address)
        {
            if (address.Id == -1)
                throw (new System.ArgumentException("Address is new instance and needs to be saved before updating."));

            ValidateRestaurantAddress(address);

            Data.RestaurantAddressSQL.UpdateRestaurantAddress(address);
        }
        /// <summary>
        /// Retrieves a RestaurantAddress instance.
        /// </summary>
        /// <param name="restaurantAddressId">The id of the address instance to retrieve.</param>
        /// <returns>A RestaurantAddress instance.</returns>
        public static RestaurantAddress GetRestaurantAddress(long restaurantId, long restaurantAddressId)
        {
            return Data.RestaurantAddressSQL.GetRestaurantAddress(restaurantId, restaurantAddressId);
        }
        /// <summary>
        /// Retrieves all address for a given Restaurant ID.
        /// </summary>
        /// <param name="restaurantId">The ID of a Restaurant to retrieve addresses for.</param>
        /// <returns>A collection of addresses for a given Restaurant.</returns>
        public static List<RestaurantAddress> GetRestaurantAddresses(long restaurantId)
        {
            return Data.RestaurantAddressSQL.GetRestaurantAddresses(restaurantId);
        }
        /// <summary>
        /// Retrieves all address for a given Restaurant.
        /// </summary>
        /// <param name="restaurant">The Restaurant to retrieve addresses for.</param>
        /// <returns>A collection of addresses for a given Restaurant.</returns>
        public static List<RestaurantAddress> GetRestaurantAddresses(Restaurant restaurant)
        {
            return Data.RestaurantAddressSQL.GetRestaurantAddresses(restaurant.Id);
        }
        /// <summary>
        /// Deletes a persisted RestaurantAddress instance.
        /// </summary>
        /// <param name="restaurantAddressId">The id of the address instance to delete.</param>
        public static void DeleteRestaurantAddress(long restaurantId, long restaurantAddressId)
        {
            Data.RestaurantAddressSQL.DeleteRestaurantAddress(restaurantId, restaurantAddressId);
        }
    }
}
