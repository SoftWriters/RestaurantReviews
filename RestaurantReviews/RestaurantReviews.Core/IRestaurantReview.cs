using System;

namespace RestaurantReviews.Core
{
    public interface IRestaurantReview
    {
        IRestaurant Restaurant { get; } //TODO: Should this just be the unique ID ?

        Guid UniqueId { get; }

        IUser Reviewer { get; }  //TODO: This should just be the unique ID

        int FiveStarRating { get; }

        string ReviewText { get; }

        DateTime Date { get; }
    }
}
