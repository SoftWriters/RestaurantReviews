using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repository;

namespace RestaurantReviews.JsonData
{
    public class Context : IContext
    {
        private const string restaurantPath = "Data/restaurants.json";
        private const string userPath = "Data/users.json";
        private const string reviewPath = "Data/reviews.json";

        public IDataSet<IRestaurant> RestaurantDataSet { get; }
        public IDataSet<IUser> UserDataSet { get; }
        public IDataSet<IReview> ReviewDataSet { get; }

        public Context()
        {
            RestaurantDataSet = new DataSet<IRestaurant>(restaurantPath);
            UserDataSet = new DataSet<IUser>(userPath);
            ReviewDataSet = new DataSet<IReview>(reviewPath);
        }
    }
}