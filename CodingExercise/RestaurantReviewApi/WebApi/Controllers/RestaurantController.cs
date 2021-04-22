using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepo;
        public RestaurantController(IRestaurantRepository restaurantRepo)
        {
            _restaurantRepo = restaurantRepo;
        }

        [HttpGet("{id}")]
        public Restaurant Get(int id)
        {
            return _restaurantRepo.ReadRestaurant(id);
        }

        [Route("GetByCity")]
        [HttpGet]
        public IList<Restaurant> GetByCity([FromQuery] int cityId)
        {
            return _restaurantRepo.ReadRestaurantsByCity(cityId);
        }

        [HttpPost]
        public UpdateResult Post([FromBody] Restaurant restaurant)
        {
            var result = new UpdateResult();
            try
            {
                var existingRestaurant = _restaurantRepo.ReadRestaurantsByCity(restaurant.CityId).Where(x => x.Name == restaurant.Name).FirstOrDefault();
                if (existingRestaurant != null)
                {
                    throw new Exception($"A restaurant named {existingRestaurant.Name} already exists for city {existingRestaurant.CityName}");
                }
                _restaurantRepo.CreateRestaurant(restaurant);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.errors.Add(ex.Message);
            }
            return result;
        }

        [Route("GetAllRestaurants")]
        [HttpGet]
        public IList<Restaurant> GetAllRestaurants()
        {
            return _restaurantRepo.ReadAllRestaurants();
        }
    }
}
