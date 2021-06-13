using RestaurantReviews.Core.Interfaces;
using System;

namespace RestaurantReviews.Core.DataTypes
{
    /// <summary>
    /// Basic In-memory implementation of IUser
    /// </summary>
    public class User : IUser
    {
        public User()
            : this(Guid.NewGuid())
        {

        }

        public User(Guid uniqueId)
        {
            UniqueId = uniqueId;
        }

        public User(IUser user)
        {
            UniqueId = user.UniqueId;
            DisplayName = user.DisplayName;
        }


        public Guid UniqueId { get; set; }

        public string DisplayName { get; set; }
    }
}
