using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantReviews.Repositories;
using RestaurantReviews.WebServices.Helpers;
using RestaurantReviews.WebServices.Models;

namespace RestaurantReviews.WebServices.Controllers
{
    public class UsersController : ApiController
    {
        private IUserRepository userRepository;
        private const int pageSize = 10;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<ReviewModel>> GetUserReviews(int id, int page = 0)
        {
            var reviews = await userRepository.GetUserReviews(id);
            return reviews.Skip(page * pageSize).Take(pageSize).Select(r => Parser.EntityToModel(r));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                userRepository.Dispose();

            base.Dispose(disposing);
        }

    }
}