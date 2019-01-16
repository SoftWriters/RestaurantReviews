using RestaurantReviews.Data;
using RestaurantReviews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews.Domain
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(string username);
        Task<bool> UserExistsAsync(int userId);
        Task<IEnumerable<User>> GetUsersAsync(int v1, int v2);
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

        public Task<IEnumerable<User>> GetUsersAsync(int page, int pagesize)
        {
            return _userDataManager.GetUsersAsync(page, pagesize);
        }

        public Task<bool> UserExistsAsync(int userId)
        {
            return _userDataManager.UserExistAsync(userId);
        }
    }
}
