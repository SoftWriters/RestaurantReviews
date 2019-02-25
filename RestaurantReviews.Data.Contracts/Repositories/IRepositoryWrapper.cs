using RestaurantReviews.Data.Contracts.Repositories.Entities;

namespace RestaurantReviews.Data.Contracts.Repositories
{
    public interface IRepositoryWrapper
    {
        IRestaurantRepository Restaurant { get; set; }

        IReviewRepository Review { get; set; }

        IUserRepository User { get; set; }
    }
}
