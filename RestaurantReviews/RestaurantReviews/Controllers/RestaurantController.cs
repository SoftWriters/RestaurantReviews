using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repositories;

namespace RestaurantReviews.Controllers
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

            AppDataLoader.LoadData(ChainRepository, CityRepository, RestaurantRepository, ReviewRepository, UserRepository);
        }

        // GET: api/Restaurant
        [Route("restaurants")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(JsonConvert.SerializeObject(RestaurantRepository.GetAllRestaurants()));
        }

        // GET: api/Restaurant/5
        [Route("restaurant/{id}")]
        [HttpGet]
        public string GetRestaurant(int id)
        {
            return JsonConvert.SerializeObject(RestaurantRepository.GetRestaurant(id));
        }

        // POST: api/Restaurant
        [Route("restaurant")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
            JObject obj = JObject.Parse(value); // We could try something like AutoMapper, but for the purposes of this exercise, the City and Chain objects are coming in as IDs.  So we have to do the mapping manually.

        }

        // DELETE: api/ApiWithActions/5
        [Route("restaurant")]
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
