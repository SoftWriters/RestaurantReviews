using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantReviews.Data.Models;

namespace RestaurantReviews.Data.Framework.RepoContracts
{
    public interface IUserRepo
    {
        User Get(long userId);
        Task<List<User>> Query(string username);
    }
}
