using System;

namespace RestaurantReviews.Core
{
    /// <summary>
    /// Representation of a restaurant
    /// </summary>
    public interface IRestaurant
    {
        Guid UniqueId { get; }

        string Name { get; }
        
        string Description { get; }

        /// <summary>
        /// Address for the restaurant
        /// Using the whole object here so that addresses can be implicitly added for new restaurants. 
        /// This is not intended to be an address management database.
        /// </summary>
        IAddress Address { get; }
    }
}
