using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Api.Model;
using RestaurantReviews.Data;
using User = RestaurantReviews.Api.Model.User;

namespace RestaurantReviews.Controllers
{
    public class UserController : ControllerBase
    {
        [HttpGet("user/{userid}")]
        public IActionResult GetUserById(Guid userId)
        {
            User user = UserDAO.GetUserById(userId);

            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }

        [HttpPost("user/create/{name}/{phoneNumber}")]
        public IActionResult Add(string name, string phoneNumber)
        {
            User user = UserDAO.Add(name, phoneNumber);

            if (user != null)
                return Created(new Uri($"user/create/{user.UserId}", UriKind.Relative), user);
            else
                return Conflict("Already Exists");
        }

        [HttpDelete("user/delete/{userid}")]
        public IActionResult Delete(Guid userId)
        {
            if (UserDAO.Delete(userId))
                return Ok();
            else
                return NotFound();
        }
    }
}