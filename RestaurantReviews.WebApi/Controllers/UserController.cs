using System;
using System.Collections.Generic;
using System.Web.Http;
using RestaurantReviews.Data;

namespace UserReviews.WebApi.Controllers
{
    public class UserController : ApiController
    {
        public RestaurantReviews.Service.Interfaces.IUserService _Service { get; set; }

        public UserController()
        {
            _Service = new RestaurantReviews.Service.UserService();
        }

        // GET api/User
        public List<User> GetAll(string category)
        {
            if(category != "all")
            {
                throw new NotImplementedException();
            }

            return _Service.GetAll();
        }

        // GET api/User/all/5
        public User Get(string category, Guid id)
        {
            if (category != "all")
            {
                throw new NotImplementedException();
            }

            return _Service.GetByID(id);
        }

        // POST api/User
        public void Post([FromBody]User user)
        {
            _Service.Save(user);
        }

        // DELETE api/User/5
        public void Delete(Guid id)
        {
            _Service.Delete(id);
        }
    }
}