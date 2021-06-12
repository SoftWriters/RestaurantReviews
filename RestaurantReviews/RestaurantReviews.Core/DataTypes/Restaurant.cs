using RestaurantReviews.Core.Interfaces;
using System;

namespace RestaurantReviews.Core.DataTypes
{
    /// <summary>
    /// Basic In-memory implementation of IRestaurant
    /// </summary>
    public class Restaurant : IRestaurant
    {
        public Restaurant()
            : this(Guid.NewGuid())
        {

        }

        public Restaurant(Guid uniqueId)
        {
            UniqueId = uniqueId;
        }

        public Guid UniqueId { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IAddress Address { get; set; }
    }
}
