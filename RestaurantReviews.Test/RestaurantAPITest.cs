using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using RestaurantReviews.Models;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Test
{
    public class RestaurantAPITest
    {
        private readonly TestServer server_;
        private readonly HttpClient client_;

        public RestaurantAPITest()
        {
            // Arrange
            server_ = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            client_ = server_.CreateClient();
        }

        [Fact]
        public void CanGetAllRestaurants()
        {
            // Act
            List<Restaurant> models = null;
            Restaurant model = null;
            HttpResponseMessage response = null;

            var task = client_.GetAsync("/api/restaurants")
                .ContinueWith((taskwithresponse) =>
                {
                    response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    models = JsonConvert.DeserializeObject<List<Restaurant>>(jsonString.Result);
                });
            task.Wait();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(3, models.Count());

            task = client_.GetAsync("/api/restaurants/1")
                .ContinueWith((taskwithresponse) =>
                {
                    response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<Restaurant>(jsonString.Result);
                });
            task.Wait();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(model.Name, "Eat'n Park");

            task = client_.GetAsync("/api/restaurants?city=Pittsburgh")
                .ContinueWith((taskwithresponse) =>
                {
                    response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    models = JsonConvert.DeserializeObject<List<Restaurant>>(jsonString.Result);
                });
            task.Wait();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(models.Count() == 1);
            Assert.Equal(models[0].Name, "Little Tokyo");
        }

        [Fact]
        public void CanCreateRestaurants()
        {
            Restaurant model = null;
            HttpResponseMessage response = null;

            var task = client_.PostAsync("/api/restaurants", new StringContent(
                    JsonConvert.SerializeObject(new Restaurant() { 
                    Name = "TestRest",
                    Street = "TestStreet",
                    City = "TestCity",
                    State = "PA",
                    Zip = "00000",
                    Country = "USA" }).ToString(), Encoding.UTF8, "application/json"))
                .ContinueWith((taskwithresponse) =>
                {
                    response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<Restaurant>(jsonString.Result);
                });
            task.Wait();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(model.RestaurantId);
            Console.WriteLine("New RestaurantId: " + model.RestaurantId.ToString());

            task = client_.PutAsync("/api/restaurants", new StringContent(
                    JsonConvert.SerializeObject(new Restaurant() { 
                    Name = "TestRest 2",
                    Street = "TestStreet",
                    City = "TestCity",
                    State = "PA",
                    Zip = "00000",
                    Country = "USA" }).ToString(), Encoding.UTF8, "application/json"))
                .ContinueWith((taskwithresponse) =>
                {
                    response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<Restaurant>(jsonString.Result);
                });
            task.Wait();

            task = client_.GetAsync("/api/restaurants/" + model.RestaurantId.ToString())
                .ContinueWith((taskwithresponse) =>
                {
                    response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<Restaurant>(jsonString.Result);
                });
            task.Wait();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(model.Name, "TestRest 2");
        }
    }
}
