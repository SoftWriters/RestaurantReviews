using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DTOs.Validators;
using DTOs;

namespace RestaurantReviewsAPI.Controllers
{
  [Route("api/restaurants")]
  [ApiController]
  public class RestaurantController : AbstractContoller
  {
    private readonly ILogger<RestaurantController> _logger;
    private readonly IRestaurantRepo _restaurantRepository;
    private readonly IValidator<RestaurantDTO> _payloadValidator;
    public RestaurantController(IRestaurantRepo restaurantRepo, ILogger<RestaurantController> logger)
    {
      _restaurantRepository = restaurantRepo;
      _logger = logger;
      _payloadValidator = new RestaurantValidator();
    } 

    [Route("{cityId}")]
    [HttpGet]
    public async Task<IActionResult> getRestaurants(long cityId, [FromQuery]string cuisine = "", [FromQuery] bool sortByAvg = false)
    {
      return await toHttpResponseWithPayload(() => _restaurantRepository.GetRestaurants(cityId,cuisine,sortByAvg), _logger);
    }

    [Route("add")]
    [HttpPost]
    public async Task<IActionResult> AddRestaurant([FromBody] RestaurantDTO newRestaurant)
    {
      return await toHttpResponse(() => { 
        _payloadValidator.ValidateData(newRestaurant);
        return _restaurantRepository.AddRestaruantAsync(newRestaurant);
      }, _logger);
    }
  }
}