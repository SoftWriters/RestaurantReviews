using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviewApi.Controllers
{
    [ApiController]
    public class SetupController : ControllerBase
    {

        public SetupController() { }

        [Route("[controller]")]
        [HttpGet]
        public string setup()
        {

            if (!System.IO.File.Exists(BaseRepository.ResturantReviewDbFile))
            {
                BaseRepository.CreateDatabase();

                BaseRepository.InsertCityData();
                BaseRepository.InsertUserData();
            }

            return "";
        }
    }
}
