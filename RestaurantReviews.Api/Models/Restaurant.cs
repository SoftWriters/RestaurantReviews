
using Remotion.Linq.Clauses;

namespace RestaurantReviews.Api.Models
{
    /// <summary>
    /// A Restaurant you can see the reviews for, if we have any.
    /// </summary>
    public class Restaurant : NewRestaurant
    {
        /// <summary>
        /// The internal identifier for this Restaurant.
        /// </summary>
        public long Id { get; set; }

        public static Restaurant FromNew(long newId, NewRestaurant newRestaurant)
        {
            return new Restaurant
            {
                Id = newId,
                Name = newRestaurant.Name,
                Description = newRestaurant.Description,
                City = newRestaurant.City,
                State = newRestaurant.State
            };
        }
    }
}