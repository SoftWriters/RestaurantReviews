using RestaurantReview.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace RestaurantReview.API.Controllers.Tests
{
	public class ReviewsControllerTests
	{
		private readonly ITestOutputHelper output;
		private readonly IConfiguration configuration;
		private readonly string path;

		public ReviewsControllerTests(ITestOutputHelper output)
		{
			// Arrange
			this.output = output;
			ServiceProvider serviceProvider = TestServiceProvider.GetProvider();
			configuration = serviceProvider.GetService<IConfiguration>();
			path = AppDomain.CurrentDomain.BaseDirectory;
		}

		[Theory()]
		[InlineData("123")]
		public void UserTest(string id)
		{
			var controller = new ReviewsController(configuration);
			controller.ControllerContext.HttpContext = new DefaultHttpContext();
			controller.ControllerContext.HttpContext.Request.Headers["ApiKey"] = configuration["ApiKey"];

			// Act
			var result = controller.User(id) as OkObjectResult;

			// Assert
			Assert.Contains(id, result.Value.ToString());
			output.WriteLine(result.Value.ToString());
		}

		[Fact()]
		public async Task AddTestAsync()
		{
			// Arrange
			JObject jobj = NewReview();
			var controller = new ReviewsController(configuration);
			controller.ControllerContext.HttpContext = new DefaultHttpContext();
			controller.ControllerContext.HttpContext.Request.Headers["ApiKey"] = configuration["ApiKey"];

			// Act
			await controller.AddAsync(jobj);
			var newJson = File.ReadAllText($"{path}/Data/Reviews.json");

			// Assert
			Assert.Contains(jobj["review_id"].ToString(), newJson);
			output.WriteLine(newJson);
		}

		[Theory]
		[InlineData("4460")]
		public void RemoveAsyncTest(string reviewId)
		{
			// Arrange
			var controller = new ReviewsController(configuration);
			controller.ControllerContext.HttpContext = new DefaultHttpContext();
			controller.ControllerContext.HttpContext.Request.Headers["ApiKey"] = configuration["ApiKey"];

			// Act
			var result = controller.RemoveAsync(reviewId);
			var newJson = File.ReadAllText($"{path}/Data/Reviews.json");

			// Assert
			Assert.DoesNotContain(reviewId, newJson);
			output.WriteLine(newJson);
		}

		private JObject NewReview()
		{
			string json = "{\"user_id\":\"54321\", \"restaurant_id\":\"4055128080014360\", \"review_id\":\"4461\", \"review\":\"Test\"}";
			JObject jObject = JObject.Parse(json);

			return jObject;
		}
	}
}