using RestaurantReviews.Domain;
using RestaurantReviews.Entity;
using RestaurantReviews.Web.Api.Security;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantReviews.Web.Api.Controllers
{
    public class UsersController : ApiController
    {
        private IUserRepository _userRepository;
        
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        // POST: api/User
        public Task<User> Post([FromBody] string username)
        {
            return _userRepository.CreateUserAsync(username);
        }

        /// <summary>
        /// Get users list
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        // GET: api/User
        [SimpleBearerTokenAuthFilterAttribute]
        [Route("api/Users")]
        public Task<IEnumerable<User>> Get(int? page, int? pagesize)
        {
            return _userRepository.GetUsersAsync(page ?? 1, pagesize ?? 1000);
        }

        

    }
}
