using RestaurantReviews.API.Models;

namespace RestaurantReviews.API.Repository
{
    public class Context : IContext
    {
        private const string restaurantPath = "Data/restaurants.json";
        private const string userPath = "Data/users.json";
        private const string reviewPath = "Data/reviews.json";

        public IDataSet<Restaurant> RestaurantDataSet { get; }
        public IDataSet<User> UserDataSet { get; }
        public IDataSet<Review> ReviewDataSet { get; }

        public Context()
        {
            RestaurantDataSet = new DataSet<Restaurant>(restaurantPath);
            UserDataSet = new DataSet<User>(userPath);
            ReviewDataSet = new DataSet<Review>(reviewPath);
        }
    }
}