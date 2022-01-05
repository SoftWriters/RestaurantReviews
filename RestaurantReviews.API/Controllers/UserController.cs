using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Interfaces.Repositories;
using RestaurantReviews.Models;
using System.Collections.Generic;

namespace UserReviews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_repository.GetAll());
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            return Ok(_repository.GetById(id));
        }

        // POST api/users
        [HttpPost]
        public void Post([FromBody] User user)
        {
            _repository.Create(user);
        }
    }
}
