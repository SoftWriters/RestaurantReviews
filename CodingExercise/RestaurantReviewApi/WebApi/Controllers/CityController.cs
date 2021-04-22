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
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepo;
        public CityController(ICityRepository cityRepo)
        {
            _cityRepo = cityRepo;
        }

        [Route("GetAllCities")]
        [HttpGet]
        public IList<City> GetAllCities()
        {
            return _cityRepo.ReadAllCities();
        }

    }
}
