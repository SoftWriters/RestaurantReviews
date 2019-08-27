using RestaurantReviews.Interfaces.Factories;
using RestaurantReviews.Interfaces.Repository;
using RestaurantReviews.JsonData.Repositories;

namespace RestaurantReviews.JsonData.Factories
{
    public class DataFactory : IDataFactory
    {
        public IRestaurantRepository RestaurantRepo { get; }
        public IReviewRepository ReviewRepo { get; }
        public IUserRepository UserRepo { get; }

        public DataFactory()
        {
            var context = new Context();
            RestaurantRepo = new RestaurantRepository(context);
            ReviewRepo = new ReviewRepository(context);
            UserRepo = new UserRepository(context);
        }
    }
}
