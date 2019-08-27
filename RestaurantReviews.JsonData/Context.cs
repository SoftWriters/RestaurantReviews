using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repository;

namespace RestaurantReviews.JsonData
{
    internal class Context
    {
        private const string restaurantPath = "Data/restaurants.json";
        private const string userPath = "Data/users.json";
        private const string reviewPath = "Data/reviews.json";

        internal DataSet<IRestaurant> RestaurantDataSet { get; }
        internal DataSet<IUser> UserDataSet { get; }
        internal DataSet<IReview> ReviewDataSet { get; }

        internal Context()
        {
            RestaurantDataSet = new DataSet<IRestaurant>(restaurantPath);
            UserDataSet = new DataSet<IUser>(userPath);
            ReviewDataSet = new DataSet<IReview>(reviewPath);
        }
    }
}