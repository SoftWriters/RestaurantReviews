using RestaurantReviews.Interfaces.Models;

namespace RestaurantReviews.Interfaces.Repository
{
    public interface IContext
    {
        IDataSet<IRestaurant> RestaurantDataSet { get; }
        IDataSet<IUser> UserDataSet { get; }
        IDataSet<IReview> ReviewDataSet { get; }
    }
}