using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReview.Models
{
    public class Restaurants
    {
        /// <summary>
        /// Internal Id of the Restaurant for the db
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// / Restaurant Name
        /// </summary>
        public string RestaurantName { get; set; }
        /// <summary>
        /// Restaurant Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Restaurant City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Restaurant State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Restaurant Zip Code
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// Restaurant Price Range. To be listed as "$" up to "$$$$$"
        /// </summary>
        public int PriceRange { get; set; }
        /// <summary>
        /// Restaurant Type - American,Japanese,Mexican, etc...
        /// </summary>
        public string RestaurantType { get; set; }
        /// <summary>
        /// Restaurant Family Friendly
        /// </summary>
        public string RestaurantFamilyFriendly { get; set; }
        /// <summary>
        /// Customer Reviews
        /// </summary>
        public string CustomerReview { get; set; }
        /// <summary>
        /// Customer Name, or userid, if entered...
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Restaurant rating by Customers
        /// </summary>
        public string RestaurantRating { get; set; }
        /// <summary>
        /// Restaurant Phone Number
        /// </summary>
        public string RestaurantPhone { get; set; }
        /// <summary>
        /// Restaurant Web Site
        /// </summary>
        public string RestaurantWebSite { get; set; }
    }
}