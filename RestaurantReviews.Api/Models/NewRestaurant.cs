namespace RestaurantReviews.Api.Models
{
    /// <summary>
    /// A new Restaurant for our review system.
    /// </summary>
    public class NewRestaurant
    {
        /// <summary>
        /// The Name of the restaurant
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// A Description of the restaurant.
        /// </summary>
        public string Description { get; set;}

        /// <summary>
        /// The City where the restaurant is located.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The State where the restaurant is located.
        /// </summary>
        public string State { get; set; }
    }
}