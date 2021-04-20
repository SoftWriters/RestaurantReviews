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
	public class JsonRestaurantTest
	{
		private readonly ITestOutputHelper output;
		private readonly string path;
		private readonly string json;

		public JsonRestaurantTest(ITestOutputHelper output)
		{
			this.output = output;
			this.path = AppDomain.CurrentDomain.BaseDirectory;
			this.json = File.ReadAllText($"{path}/Data/Restaurants.json");
		}

		[Fact]
		public void ReadFileTest()
		{
			// Act
			dynamic obj = JsonConvert.DeserializeObject(json);
			var name = obj[0].restaurant_name.ToString();

			// Assert
			Assert.Equal("Olive Garden", name);
			output.WriteLine(json);
		}

		[Fact]
		public async Task AddObjectToJsonTestAsync()
		{
			// Act
			var arr = JArray.Parse(json);
			var itemToAdd = new JObject
			{
				["restaurant_name"] = "Wei's Kitchen",
				["restaurant_phone"] = "412-123-4567",
				["address"] = "Unknown",
				["restaurant_id"] = 1
			};
			arr.Add(itemToAdd);
			var newJson = JsonConvert.SerializeObject(arr, Formatting.Indented);
			using (var outputFile = new StreamWriter($"{path}/Data/Restaurants.json"))
			{
				await outputFile.WriteAsync(newJson);
			}
			newJson = File.ReadAllText($"{path}/Data/Restaurants.json");

			// Assert
			Assert.Contains("Wei's Kitchen", newJson);
			output.WriteLine(newJson);
		}

		[Fact]
		public async Task DeleteObjectFromJsonTestAsync()
		{
			// Arrange
			var jsonArray = JArray.Parse(json);
			var jsonObjects = jsonArray.OfType<JObject>().ToList();

			// Act
			foreach (var item in jsonObjects.ToList())
			{
				var name = item["restaurant_name"].ToString();
				if (name == "Wei's Kitchen")
				{
					jsonObjects.Remove(item);
				}
			}
			var newJson = JsonConvert.SerializeObject(jsonObjects);
			using (var outputFile = new StreamWriter($"{path}/Data/Restaurants.json"))
			{
				await outputFile.WriteAsync(newJson);
			}
			newJson = File.ReadAllText($"{path}/Data/Restaurants.json");

			// Assert
			Assert.DoesNotContain("Wei's Kitchen", newJson);
			output.WriteLine(newJson);
		}

		[Theory]
		[InlineData("4055128080014360")]
		public void GetObjectByKeyTest(string id)
		{
			// Arrange
			var jsonArray = JArray.Parse(json);
			var jsonObjects = jsonArray.OfType<JObject>().ToList();
			var newObj = new object();

			// Act
			foreach (var item in jsonObjects)
			{
				if (id == item["restaurant_id"].ToString())
				{
					newObj = item;
				}
			}

			var newJson = JsonConvert.SerializeObject(newObj);

			// Assert
			Assert.Contains(id, newJson);
			output.WriteLine(newJson);
		}

		[Theory]
		[InlineData("4055128080014360")]
		public void GetReviewByRestaurantIdTest(string id)
		{
			// Arrange
			var jsonArray = JArray.Parse(json);
			var jsonObjects = jsonArray.OfType<JObject>().ToList();
			var newObj = new object();

			// Act
			foreach (var item in jsonObjects)
			{
				if (id == item["restaurant_id"].ToString())
				{
					newObj = item["review"];
				}
			}

			var newJson = JsonConvert.SerializeObject(newObj);

			// Assert
			//Assert.Contains(id, newJson);
			output.WriteLine(newJson);
		}

		[Theory(Skip = "Moved to reviews.json")]
		[InlineData("4055128080014360")]
		public void PostReviewByRestaurantIdTest(string id)
		{
			// Arrange
			var jToken = JToken.Parse(json);
			var newReview = new JObject
			{
				["user_id"] = "123",
				["user_name"] = "Unknown",
				["review_id"] = "4459",
				["comment"] = "Test1"
			};
			var matches = jToken.Select(x => x["restaurant_id"])
													.Where(x => x.Value<string>() == id)
													.ToList();

			var node = matches[0].Parent.Parent["review"].Parent;
			node[0].Append(newReview);

			var newJson = jToken.ToString(Formatting.Indented);

			// Assert
			Assert.Contains("4459", newJson);
			output.WriteLine(newJson);
		}

		[Theory]
		[InlineData("123")]
		public void GetReviewsByUserIdTest(string id)
		{
			// Arrange
			var jsonArray = JArray.Parse(json);
			var jsonObjects = jsonArray.OfType<JObject>().ToList();
			var newObj = new List<object>();

			// Act
			foreach (var item in jsonObjects)
			{
				foreach (var rev in item["review"])
				{
					if (id == rev["user_id"].ToString())
					{
						newObj.Add(item["review"]);
					}
				}
			}

			var newJson = JsonConvert.SerializeObject(newObj);

			// Assert
			Assert.Contains(id, newJson);
			output.WriteLine(newJson);
		}

		[Theory]
		[InlineData("4568")]
		public void DeleteReviewByReviewIdTest(string id)
		{
			// Arrange
			var jToken = JToken.Parse(json);
			var result = jToken.SelectMany(x => x["review"])
													.Select(x => x["review_id"])
													.Where(x => x.Value<string>() == id)
													.ToList();

			// Act
			result[0].Parent.Parent.Remove();

			var newJson = jToken.ToString(Formatting.Indented);

			// Assert
			Assert.DoesNotContain(id, newJson);
			output.WriteLine(newJson);
		}
	}
}