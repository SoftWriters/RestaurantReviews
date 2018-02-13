using RestaurantReviews.Data.Models;
using RestaurantReviews.Data.Models.Domain;

namespace RestaurantReviews.Data.Framework.RepoContracts
{
    public interface IUserRepo
    {
        User Get(long userId);
    }
}
