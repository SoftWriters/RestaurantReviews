using System;

namespace RestaurantReviews.Core
{
    /// <summary>
    /// Representation of a user in the review database
    /// </summary>
    /// <remarks>In reality this would have all kinds of properties for the user's profile.
    /// In this context we only really care about the display name to show with the reviews.</remarks>
    public interface IUser
    {
        Guid UniqueId { get; }

        string DisplayName { get; }
    }
}
