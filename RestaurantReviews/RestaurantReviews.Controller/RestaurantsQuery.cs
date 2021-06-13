namespace RestaurantReviews.Controller
{
    /// <summary>
    /// Parameter object for searching for restaurants 
    /// </summary>
    /// <remarks>Could add more to this, e.g. by street name, ratings</remarks>
    public class RestaurantsQuery
    {
        /// <summary>
        /// Restaurant name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Restaurant City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Restaurant state or province
        /// </summary>
        public string StateOrProvince { get; set; }

        /// <summary>
        /// Restaurant postal code
        /// </summary>
        public string PostalCode { get; set; }
    }
}
