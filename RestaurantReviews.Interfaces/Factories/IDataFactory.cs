using RestaurantReviews.Interfaces.Repository;

namespace RestaurantReviews.Interfaces.Factories
{
    public interface IDataFactory
    {
        IRestaurantRepository RestaurantRepo { get; }
        IReviewRepository ReviewRepo { get; }
        IUserRepository UserRepo { get; }
    }
}
