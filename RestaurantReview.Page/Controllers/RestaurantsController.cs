using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.Page.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RestaurantsController : ControllerBase
	{
		private readonly Logger _log = LogManager.GetCurrentClassLogger();
		private readonly HttpClient client;
		private readonly string apiUrl;
		private readonly string apiKey;

		public RestaurantsController(IConfiguration configuration, IHttpClientFactory factory)
		{
			client = factory.CreateClient();
			apiUrl = configuration["BaseUrl"];
			apiKey = configuration["APIKey"];
		}

		[HttpGet]
		[Route("{city}")]
		public async Task<IActionResult> Index(string city)
		{
			try
			{
				client.DefaultRequestHeaders.Add("ApiKey", apiKey);
				var response = await client.GetAsync($"{apiUrl}/Restaurants/{city}");
				response.EnsureSuccessStatusCode();

				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					return Ok(result);
				}

				return BadRequest(response.Content);
			}
			catch (Exception ex)
			{
				_log.Error(ex.Message);
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
				client.DefaultRequestHeaders.Add("ApiKey", apiKey);
				var content = SetStringContent(obj);
				var response = await client.PostAsync($"{apiUrl}/Restaurants/Add/{city}", content);
				response.EnsureSuccessStatusCode();

				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					return Ok(result);
				}

				return BadRequest(response.Content);
			}
			catch (Exception)
			{
				throw;
			}
		}

		private StringContent SetStringContent(JObject jObject)
		{
			var json = JsonConvert.SerializeObject(jObject);
			var strContent = new StringContent(json, Encoding.UTF8, "application/json");

			return strContent;
		}
	}
}