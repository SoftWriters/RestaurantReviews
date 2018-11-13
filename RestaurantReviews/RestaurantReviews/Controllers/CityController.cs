using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Repositories;

namespace RestaurantReviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
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

        public CityController(IChainRepository chainRepository, ICityRepository cityRepository, IRestaurantRepository restaurantRepository, IReviewRepository reviewRepository, IUserRepository userRepository)
        {
            ChainRepository = chainRepository;
            CityRepository = cityRepository;
            RestaurantRepository = restaurantRepository;
            ReviewRepository = reviewRepository;
            UserRepository = userRepository;

            AppDataLoader.LoadData(ChainRepository, CityRepository, RestaurantRepository, ReviewRepository, UserRepository);
        }

        // GET: api/City/5
        [HttpGet("{id}", Name = "GetCity")]
        public string GetCity(int id)
        {
            return "City";
        }

        // GET: api/City/5/Restaurants
        [HttpGet("{id}/Restaurants", Name = "GetRestaurantsByCity")]
        public string GetRestaurantsByCity(int id)
        {
            ICityModel city = CityRepository.FindCityById(id);
            return JsonConvert.SerializeObject(RestaurantRepository.GetRestaurantsByCity(city));
        }
    }
}