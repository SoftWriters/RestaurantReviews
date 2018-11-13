using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repositories;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantReview2.Controllers
{
    public class ReviewController : ApiController
    {
        public IRestaurantRepository RestaurantRepository
        { get; set; }

        public IReviewRepository ReviewRepository
        { get; set; }

        public IUserRepository UserRepository
        { get; set; }

        public ReviewController(IChainRepository chainRepository, ICityRepository cityRepository, IRestaurantRepository restaurantRepository, IReviewRepository reviewRepository, IUserRepository userRepository)
        {
            RestaurantRepository = restaurantRepository;
            ReviewRepository = reviewRepository;
            UserRepository = userRepository;
        }

        // DELETE: review/5
        [Route("review/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteReview(int id)
        {
            var model = ReviewRepository.GetReviewById(id);
            ReviewRepository.DeleteReview(model);
            return Ok(JsonConvert.SerializeObject(ReviewRepository.GetReviews()));
        }

        // GET: review/user/5
        [Route("review/user/{id}")]
        [HttpGet]
        public IHttpActionResult GetReviewsByUser(int id)
        {
            var user = UserRepository.GetUserById(id);
            return Ok(JsonConvert.SerializeObject(ReviewRepository.GetReviewsByUser(user)));
        }

        // GET: reviews
        [Route("reviews")]
        [HttpGet]
        public IHttpActionResult GetReviews()
        {
            return Ok(JsonConvert.SerializeObject(ReviewRepository.GetReviews()));
        }

        // POST: addreview
        [Route("addreview")]
        [HttpPost]
        // async if we had DB connection
        public IHttpActionResult PostReview(HttpRequestMessage value)
        {
            var val = value.Content.ReadAsStringAsync().Result;
            // We could try something like AutoMapper, but for the purposes of this exercise, 
            // the City and Chain objects are coming in as IDs.  Manual mapping follows:

            JObject my_obj = JsonConvert.DeserializeObject<JObject>(val);
            JToken token = JToken.Parse(val);

            ReviewModel model = JsonConvert.DeserializeObject<ReviewModel>(JsonConvert.SerializeObject(token));

            // I could create a new model and copy the values over instead of modifying this in place.  
            model.SubmittingUser = UserRepository.GetUserById(1); // With authentication in place, this would be defined by the authentication system

            if (model.Restaurant != null)
            {
                var restId = model.Restaurant.Id;
                model.Restaurant = RestaurantRepository.GetRestaurantById(restId) as RestaurantModel;
            }

            ReviewRepository.AddReview(model);

            return Ok(ReviewRepository.GetReviews());
        }
    }
}
