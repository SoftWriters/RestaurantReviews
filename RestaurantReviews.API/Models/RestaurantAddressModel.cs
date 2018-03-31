using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.API.Models
{
    /// <summary>
    /// A proxy model for RestaurantReviews.Entities.RestaurantAddress
    /// </summary>
    public class RestaurantAddressModel
    {
        /// <summary>
        /// The ID of the address.
        /// </summary>
        public long Id { get; private set; }
        /// <summary>
        /// The ID of the corresponding Restaurant.
        /// </summary>
        public long RestaurantId { get; set; }
        /// <summary>
        /// The primary street address.
        /// </summary>
        public string Street1 { get; set; }
        /// <summary>
        /// The secondary street adress.
        /// </summary>
        public string Street2 { get; set; }
        /// <summary>
        /// The city of the address.
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// The region/state of the address.
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// The postal code of the address.
        /// </summary>
        public string PostalCode { get; set; }
    }
}