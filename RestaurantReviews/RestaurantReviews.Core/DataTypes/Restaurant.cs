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

        //public Restaurant(Guid uniqueId = default(Guid), string name = null, string description = null, IAddress address = null)
        //{
        //    UniqueId = (uniqueId == default(Guid)) ? Guid.NewGuid() : uniqueId;
        //    Name = name;
        //    Description = description;
        //    Address = address;
        //}

        public Guid UniqueId { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IAddress Address { get; set; }
    }
}
