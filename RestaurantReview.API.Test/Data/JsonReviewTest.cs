using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RestaurantReview.API.Test
{
	public class JsonReviewTest
	{
		private readonly ITestOutputHelper output;
		private readonly string path;
		private readonly string json;

		public JsonReviewTest(ITestOutputHelper output)
		{
			this.output = output;
			this.path = AppDomain.CurrentDomain.BaseDirectory;
			this.json = File.ReadAllText($"{path}/Data/Reviews.json");
		}

		[Fact]
		public void ReadFileTest()
		{
			// Act
			dynamic obj = JsonConvert.DeserializeObject(json);
			var restaurant_id = obj[0].restaurant_id.ToString();

			// Assert
			Assert.Equal("4055128080014360", restaurant_id);
			output.WriteLine(json);
		}

		[Theory]
		[InlineData("4460")]
		public async Task AddReviewTestAsync(string id)
		{
			// Act
			var arr = JArray.Parse(json);
			var itemToAdd = new JObject
			{
				["user_id"] = "123",
				["restaurant_id"] = "4055128080014360",
				["review_id"] = "4460",
				["comment"] = "Test1"
			};
			arr.Add(itemToAdd);
			var newJson = JsonConvert.SerializeObject(arr, Formatting.Indented);
			using (var outputFile = new StreamWriter($"{path}/Data/Reviews.json"))
			{
				await outputFile.WriteAsync(newJson);
			}
			newJson = File.ReadAllText($"{path}/Data/Reviews.json");

			// Assert
			Assert.Contains(id, newJson);
			output.WriteLine(newJson);
		}

		[Theory]
		[InlineData("4460")]
		public async Task RemoveReviewTestAsync(string id)
		{
			// Arrange
			var jToken = JToken.Parse(json);
			var result = jToken.Select(x => x["review_id"])
													.Where(x => x.Value<string>() == id)
													.ToList();

			// Act
			result[0].Parent.Parent.Remove();
			var newJson = jToken.ToString(Formatting.Indented);
			using (var outputFile = new StreamWriter($"{path}/Data/Reviews.json"))
			{
				await outputFile.WriteAsync(newJson);
			}
			newJson = File.ReadAllText($"{path}/Data/Reviews.json");

			// Assert
			Assert.DoesNotContain(id, newJson);
			output.WriteLine(newJson);
		}
	}
}