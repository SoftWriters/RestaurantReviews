using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReview.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ReviewsController : ControllerBase
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		private readonly IConfiguration configuration;
		private readonly string path;
		private readonly string reviewJson;

		public ReviewsController(IConfiguration configuration)
		{
			this.configuration = configuration;

			// mock data
			path = AppDomain.CurrentDomain.BaseDirectory;
			reviewJson = System.IO.File.ReadAllText($"{path}/Data/Reviews.json");
		}

		[Route("[action]/{userId}")]
		public new IActionResult User(string userId)
		{
			try
			{
				var apiKey = HttpContext.Request.Headers["ApiKey"];
				var writersApiKey = configuration["ApiKey"];
				if (apiKey == writersApiKey)
				{
					string review = GetReviewFromRestaurantJson(userId);

					return Ok(review);
				}
				return Unauthorized();
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				throw;
			}
		}

		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> AddAsync([FromBody] JObject jObj)
		{
			try
			{
				dynamic json = jObj;
				var apiKey = HttpContext.Request.Headers["ApiKey"];
				var writersApiKey = configuration["ApiKey"];
				if (apiKey == writersApiKey)
				{
					await WriteToDb(jObj);
					return Ok();
				}
				return Unauthorized();
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				throw;
			}
		}

		private async Task WriteToDb(JObject jObj)
		{
			// Skip checking the dupicate record. Should be easy to do with real database.
			var arr = JArray.Parse(reviewJson);
			arr.Add(jObj);
			var newJson = JsonConvert.SerializeObject(arr, Formatting.Indented);
			using var outputFile = new StreamWriter($"{path}/Data/Reviews.json");
			await outputFile.WriteAsync(newJson);
		}

		[Route("[action]/{reviewId}")]
		public async Task RemoveAsync(string reviewId)
		{
			var jToken = JToken.Parse(reviewJson);
			var result = jToken.Select(x => x["review_id"])
													.Where(x => x.Value<string>() == reviewId)
													.ToList();
			result[0].Parent.Parent.Remove();
			var newJson = jToken.ToString(Formatting.Indented);
			using var outputFile = new StreamWriter($"{path}/Data/Reviews.json");
			await outputFile.WriteAsync(newJson);
		}

		private string GetReviewFromRestaurantJson(string userId)
		{
			var jsonArray = JArray.Parse(reviewJson);
			var jsonObjects = jsonArray.OfType<JObject>().ToList();
			var newObj = new object();

			foreach (dynamic item in jsonObjects)
			{
				if (userId == item.user_id.ToString())
				{
					newObj = item;
				}
			}

			var review = JsonConvert.SerializeObject(newObj);

			return review;
		}
	}
}