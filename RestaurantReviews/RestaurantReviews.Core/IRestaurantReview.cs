using System;

namespace RestaurantReviews.Core
{
    public interface IRestaurantReview
    {
        IRestaurant Restaurant { get; }

        Guid UniqueId { get; }

        IUser Reviewer { get; }

        int FiveStarRating { get; }

        string ReviewText { get; }

        DateTime Date { get; }
    }
}
