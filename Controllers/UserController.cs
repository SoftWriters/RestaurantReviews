
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantReviews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAsync()
        {
            var users = await _unitOfWork.Users.ListAllAsync();
            // Return Status Code 200
            return Ok(users);
        }

        [HttpPost]
        [Route("users")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> PostAsync([FromBody] UserDTO dto)
        {
            if (dto == null)
            {
                // Return Status Code 400
                return BadRequest("Missing User DTO");
            }

            var user = await _unitOfWork.Users.CreateAsync(dto);
            if (user == null)
            {
                // Return Status Code 400
                return BadRequest("Unable to Create User");
            }
            await _unitOfWork.CompleteAsync();

            // Return Status Code 201
            return Created("/users/" + user.UserID, user);
        }

        [HttpGet]
        [Route("users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAsync(long id)
        {
            var user = await _unitOfWork.Users.ReadAsync(id);
            if (user == null)
            {
                // Return Status Code 404
                return NotFound("No Record Found for User ID " + id);
            }
            await _unitOfWork.CompleteAsync();

            // Return Status Code 200
            return Ok(user);
        }
    }
}
