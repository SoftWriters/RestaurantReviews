using RestaurantReviews.API.Models;

namespace RestaurantReviews.API.Repository
{
    public interface IContext
    {
        IDataSet<Restaurant> RestaurantDataSet { get; }
        IDataSet<User> UserDataSet { get; }
        IDataSet<Review> ReviewDataSet { get; }
    }
}