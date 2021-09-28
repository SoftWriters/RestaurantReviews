using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.API.Models
{
    /// <summary>
    /// A proxy model for RestaurantReviews.Entities.Restaurant
    /// </summary>
    public class RestaurantModel
    {
        /// <summary>
        /// The ID of the Restaurant instance.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The name of the Restaurant.
        /// </summary>
        public string Name { get; set; }
    }
}