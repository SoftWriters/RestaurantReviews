
namespace RestaurantReviews.Api.Models
{
    /// <summary>
    /// A Restaurant you can see the reviews for, if we have any.
    /// </summary>
    public class Restaurant
    {
        /// <summary>
        /// The internal identifier for this Restaurant
        /// </summary>
        public long Id { get; set; }

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