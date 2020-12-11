using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Logic;
using RestaurantReviews.Logic.Model.User.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRestaurantLogic logic;

        public UserController(IRestaurantLogic logic)
        {
            this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        }

        [HttpPost]
        [Route("/query")]
        public async Task<ActionResult<UserQueryResponse>> Query([FromBody]UserQueryRequest request)
        {
            var response = await logic.UserQuery(request);
            return Ok(response);
        }
    }
}
