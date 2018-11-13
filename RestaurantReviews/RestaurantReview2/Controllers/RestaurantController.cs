using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repositories;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantReview2.Controllers
{
    public class RestaurantController : ApiController
    {
        public IChainRepository ChainRepository
        { get; set; }

        public ICityRepository CityRepository
        { get; set; }

        public IRestaurantRepository RestaurantRepository
        { get; set; }

        public IReviewRepository ReviewRepository
        { get; set; }

        public IUserRepository UserRepository
        { get; set; }
        
        public RestaurantController(IChainRepository chainRepository, ICityRepository cityRepository, IRestaurantRepository restaurantRepository, IReviewRepository reviewRepository, IUserRepository userRepository)
        {
            ChainRepository = chainRepository;
            CityRepository = cityRepository;
            RestaurantRepository = restaurantRepository;
            ReviewRepository = reviewRepository;
            UserRepository = userRepository;
        }

        // GET: restaurant
        [Route("restaurants")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(JsonConvert.SerializeObject(RestaurantRepository.GetRestaurants()));
        }

        // GET: restaurant/5
        [Route("restaurant/{id}")]
        [HttpGet]
        public IHttpActionResult GetRestaurant(int id)
        {
            return Ok(JsonConvert.SerializeObject(RestaurantRepository.GetRestaurantById(id)));
        }

        // POST: addrestaurant
        [Route("addrestaurant")]
        [HttpPost]
        // async if we had DB connection
        public IHttpActionResult PostRestaurant(HttpRequestMessage value)
        {
            var val = value.Content.ReadAsStringAsync().Result;
            // We could try something like AutoMapper, but for the purposes of this exercise, 
            // the City and Chain objects are coming in as IDs.  Manual mapping follows:

            JObject my_obj = JsonConvert.DeserializeObject<JObject>(val);
            JToken token = JToken.Parse(val);

            RestaurantModel model = JsonConvert.DeserializeObject<RestaurantModel>(JsonConvert.SerializeObject(token));

            // I could create a new model and copy the values over instead of modifying this in place.  
            if (model.City != null)
            {
                var cityId = model.City.Id;
                model.City = CityRepository.GetCityById(cityId) as CityModel;
            }

            if (model.Chain != null)
            {
                var chainId = model.Chain.Id;
                model.Chain = ChainRepository.GetChainById(chainId) as ChainModel;
            }

            RestaurantRepository.AddRestaurant(model);

            return Ok(RestaurantRepository.GetRestaurants());
        }
    }
}
