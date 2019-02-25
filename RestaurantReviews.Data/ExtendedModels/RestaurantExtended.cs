using RestaurantReviews.Data.Entities;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Data.ExtendedModels
{
    public class RestaurantExtended
    {
        #region Concrete Properties

        public Guid Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string EmailAddress { get; set; }

        public bool IsConfirmed { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string PostalCode { get; set; }

        public string State { get; set; }

        public string WebsiteUrl { get; set; }

        #endregion Concrete Properties

        #region Navigation Properties

        public IEnumerable<Review> Reviews { get; set; }

        #endregion Navigation Properties

        #region Constructors

        public RestaurantExtended()
        {
        }

        public RestaurantExtended(Restaurant restaurant)
        {
            Id = restaurant.Id;
            Address = restaurant.Address;
            City = restaurant.City;
            Country = restaurant.Country;
            EmailAddress = restaurant.EmailAddress;
            IsConfirmed = restaurant.IsConfirmed;
            Name = restaurant.Name;
            Phone = restaurant.Phone;
            PostalCode = restaurant.PostalCode;
            State = restaurant.State;
            WebsiteUrl = restaurant.WebsiteUrl;
        }

        #endregion Constructors
    }
}
