using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using RestaurantReviews.Classes;
using RestaurantReviews.Repositories;
using RestaurantReviews.WebServices.Helpers;
using RestaurantReviews.WebServices.Models;

namespace RestaurantReviews.WebServices.Controllers
{
    public class RestaurantsController : ApiController
    {
        private IRestaurantRepository restaurantRepository;
        private const int pageSize = 10;

        public RestaurantsController(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }

        public async Task<IEnumerable<RestaurantModel>> GetRestaurants(int page = 0)
        {
            var restaurants = await restaurantRepository.GetAllRestaurants();
            return restaurants.Skip(page * pageSize).Take(pageSize).Select(r => Parser.EntityToModel(r));
        }

        public async Task<IHttpActionResult> GetRestaurant(int id)
        {
            var restaurant = await restaurantRepository.GetRestaurant(id);

            if (restaurant == null)
                return NotFound();

            return Ok(Parser.EntityToModel(restaurant));
        }

        public async Task<IEnumerable<RestaurantModel>> GetRestaurants(string city, string state = null, string zipCode = null, int page = 0)
        {
            var restaurants = await restaurantRepository.GetRestaurantsByLocation(new RestaurantLocation { City = city, State = state, ZipCode = zipCode });
            return restaurants.Skip(page * pageSize).Take(pageSize).Select(r => Parser.EntityToModel(r));
        }

        public async Task<IHttpActionResult> PostRestaurant(RestaurantModel restaurant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            restaurant.Id = await restaurantRepository.Add(Parser.ModelToEntity(restaurant));

            return CreatedAtRoute("MyApi", new { id = restaurant.Id }, restaurant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                restaurantRepository.Dispose();

            base.Dispose(disposing);
        }

    }
}