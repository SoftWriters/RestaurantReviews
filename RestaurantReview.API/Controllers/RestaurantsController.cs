using Google.Apis.Sheets.v4;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RestaurantReview.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RestaurantsController : ControllerBase
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		private readonly IConfiguration configuration;
		private readonly string path;
		private readonly string restaurantJson;

		public RestaurantsController(IConfiguration configuration)
		{
			this.configuration = configuration;

			// mock data
			path = AppDomain.CurrentDomain.BaseDirectory;
			restaurantJson = System.IO.File.ReadAllText($"{path}/Data/Restaurants.json");
		}

		[Route("{city}")]
		public IActionResult Index(string city)
		{
			try
			{
				var apiKey = HttpContext.Request.Headers["ApiKey"];
				var writersApiKey = configuration["ApiKey"];
				if (apiKey == writersApiKey)
				{
					return Ok(restaurantJson);
				}
				return Unauthorized();
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				throw;
			}
		}

		[HttpPost]
		[Route("[action]/{city}")]
		public async Task<IActionResult> AddAsync(string city, [FromBody] JObject obj)
		{
			try
			{
				dynamic json = obj;
				var apiKey = HttpContext.Request.Headers["ApiKey"];
				var writersApiKey = configuration["ApiKey"];
				if (apiKey == writersApiKey)
				{
					var arr = JArray.Parse(restaurantJson);
					arr.Add(obj);
					var newJson = JsonConvert.SerializeObject(arr, Formatting.Indented);
					using (var outputFile = new StreamWriter($"{path}/Data/Restaurants.json"))
					{
						await outputFile.WriteAsync(newJson);
					}
					return Ok();
				}
				return Unauthorized();
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}