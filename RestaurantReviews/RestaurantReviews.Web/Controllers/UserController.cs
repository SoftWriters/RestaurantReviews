using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Logic;
using RestaurantReviews.Logic.Model;
using RestaurantReviews.Logic.Model.User.Search;
using RestaurantReviews.Web.Results;
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

        /// <summary>
        /// Searches for users based on the specified criteria
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("search")]
        [ProducesResponseType(typeof(SearchResponse<SearchUser>), 200)]
        public async Task<SearchActionResult<SearchUser>> Query([FromBody]SearchUserRequest request)
        {
            var response = await logic.SearchUsers(request);
            return new SearchActionResult<SearchUser>(response);
        }
    }
}
