using RestaurantReviews.Data;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace RestaurantReviews.WebApi.Controllers
{
    public class RestaurantController : ApiController
    {
        public Service.Interfaces.IRestaurantService _Service { get; set; }

        public RestaurantController()
        {
            _Service = new Service.RestaurantService();
        }

        // GET api/Restaurant
        [HttpGet]
        public List<Restaurant> GetAll()
        {
            return _Service.GetAll();
        }

        // GET api/Restaurant/all/5
        [HttpGet]
        public Restaurant GetByID(string category, Guid id)
        {
            if(category != "all")
            {
                throw new NotImplementedException();
            }

            return _Service.GetByID(id);
        }

        // Get api/Restaurant/pittsburgh
        [HttpGet]
        public List<Restaurant> GetByCity(string category)
        {
            var guid = Guid.NewGuid();

            if (category == "all")
            {
                return GetAll();
            }
            
            return _Service.GetByCity(category);
        }

        // POST api/Restaurant
        public void Post([FromBody]Restaurant restaurant)
        {
            _Service.Save(restaurant);
        }
        
        // DELETE api/Restaurant/5
        public void Delete(Guid id)
        {
            _Service.Delete(id);
        }
    }
}