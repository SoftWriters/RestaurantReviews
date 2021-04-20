using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RestaurantReview.API.Controllers.Tests
{
	public class RestaurantsControllerTests
	{
		private readonly ITestOutputHelper output;
		private readonly IConfiguration configuration;
		private readonly IHttpClientFactory clientFactory;
		private readonly HttpClient client;

		public RestaurantsControllerTests(ITestOutputHelper output)
		{
			// Arrange
			this.output = output;
			ServiceProvider serviceProvider = TestServiceProvider.GetProvider();
			configuration = serviceProvider.GetService<IConfiguration>();
			clientFactory = serviceProvider.GetService<IHttpClientFactory>();
			client = clientFactory.CreateClient();
		}

		[Theory()]
		[InlineData("Pittsburgh")]
		public void IndexTestAsync(string city)
		{
			// Arrange
			var controller = new RestaurantsController(configuration);
			controller.ControllerContext.HttpContext = new DefaultHttpContext();
			controller.ControllerContext.HttpContext.Request.Headers["ApiKey"] = configuration["ApiKey"];

			// Act
			dynamic result = controller.Index(city) as OkObjectResult;
			var res = result.Value;

			// Assert
			//Assert.Contains("Olive Garden", result);
			output.WriteLine(res.ToString());
		}

		[Theory]
		[InlineData("https://spreadsheets.google.com/feeds/cells/1lTxqP_yrRJoueiq3HXJ_C6w96gv2l664uGk6TwmGNN4/1/public/full?alt=json")]
		public async Task GoogleSheetJsonTest(string url)
		{
			// Act
			using var response = await client.GetAsync(url);
			var result = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.Contains("Olive Garden", result);
			output.WriteLine(result);
		}
	}
}