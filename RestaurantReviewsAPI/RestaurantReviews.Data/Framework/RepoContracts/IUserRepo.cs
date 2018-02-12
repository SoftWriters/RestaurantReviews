using RestaurantReviews.Data.Models;

namespace RestaurantReviews.Data.Framework.RepoContracts
{
    public interface IUserRepo
    {
        void Add(User user);
        User Get(long userId);
    }
}
