using RestaurantReviews.Data.Models;

namespace RestaurantReviews.Data.Framework.RepoContracts
{
    public interface IUserRepo
    {
        User Get(long userId);
    }
}
