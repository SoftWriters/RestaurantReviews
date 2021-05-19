using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestarauntReviews.DTO;
using RestarauntReviews.Service;
using RestarauntReviews.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestarauntReviews.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RestaurantReviewsController : ControllerBase
    {
        private readonly ILogger<RestaurantReviewsController> logger;
        private IRestaurantReviewService service;
        public RestaurantReviewsController(ILogger<RestaurantReviewsController> _logger, IRestaurantReviewService _service)
        {
            logger = _logger;
            service = _service;
        }

        [HttpGet]
        public IEnumerable<Restaraunt> GetRestaurants([FromBody] string city)
        {
            try
            {
                return service.GetRestaraunts(city);
            } 
            catch (Exception ex)
            {
                logger.LogError("Error in GetRestaurants by City:" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);

                throw(ex);
            }

            
        }

        [HttpGet]
        public IEnumerable<Review> GetReviews([FromBody] string username)
        {
            try
            {
                return service.GetReviews(username);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in GetRestaurants by City:" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);

                throw (ex);
            }


        }

        [HttpPost]
        public void AddReview([FromBody] Review review)
        {
            try
            {
                service.AddReview(review);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in AddReview:" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
                throw (ex);
            }
        }

        [HttpPost]
        public void AddRestaurant([FromBody] Restaraunt restaraunt)
        {
            try
            {
                service.AddRestaurant(restaraunt);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in AddRestaurant:" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
                throw (ex);
            }
        }
    }
}
