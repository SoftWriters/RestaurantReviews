using RestaurantReviews.Data;
using RestaurantReviews.Entity;
using System.Threading.Tasks;

namespace RestaurantReviews.Domain
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(string username);
    }
    public class UserRepository : IUserRepository
    {
        private IUserDataManager _userDataManager;

        public UserRepository(IUserDataManager userDataManager)
        {
            _userDataManager = userDataManager;
        }

        public Task<User> CreateUserAsync(string username)
        {
            return _userDataManager.CreateUserAsync(username);
        }
    }
}
