using RestaurantReviews.Domain;
using RestaurantReviews.Entity;
using RestaurantReviews.Web.Api.ExceptionHandling;
using RestaurantReviews.Web.Api.Security;
using RestaurantReviews.Web.Api.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding.Binders;

namespace RestaurantReviews.Web.Api.Controllers
{
    [ModelValidationFilter]
    [DuplicateKeyExceptionFilter]
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
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: api/User
        public Task<User> Post([FromBody] User user)
        {
            return _userRepository.CreateUserAsync(user.UserName);
        }

        /// <summary>
        /// Get users list
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        // GET: api/User
        [SimpleBearerTokenAuthFilter]
        public Task<IEnumerable<User>> Get(
            [FromUri(BinderType = typeof(TypeConverterModelBinder))] int? page,
            [FromUri(BinderType = typeof(TypeConverterModelBinder))]int? pagesize)
        {
            return _userRepository.GetUsersAsync(page ?? 1, pagesize ?? 1000);
        }

        

    }
}
