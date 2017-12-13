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
using System.Net.Http.Headers;

namespace RestaurantReviews.Test
{
    public class RestaurantAPITest
    {
        private readonly TestServer server_;
        private readonly HttpClient client_;
        private readonly string accesstoken_;

        public RestaurantAPITest()
        {
            // Arrange
            server_ = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            client_ = server_.CreateClient();

            dynamic tokenObj = null;
            HttpResponseMessage response = null;

            var task = client_.PostAsync("/api/JWT/GenerateToken", 
                new StringContent("{\"email\": \"demouser1@demo.com\", \"pass\": \"demopass\"}"))
                .ContinueWith((taskwithresponse) =>
                {
                    response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    tokenObj = JsonConvert.DeserializeObject<dynamic>(jsonString.Result);
                });
            task.Wait();

            if (tokenObj != null)
            {
                accesstoken_ = tokenObj["token"];
                client_.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", accesstoken_ );
            }
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
            Assert.Equal("Eat'n Park", model.Name);

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
            Assert.Equal("Little Tokyo", models[0].Name);
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
            Assert.NotNull(model);

            Console.WriteLine("New RestaurantId: " + model.RestaurantId.ToString());

            task = client_.PutAsync("/api/restaurants/" + model.RestaurantId.ToString(), new StringContent(
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

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("TestRest 2", model.Name);

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
            Assert.Equal("TestRest 2", model.Name);
        }

        [Fact]
        public void CanGetReviews()
        {
            Review model = null;
            List<Review> models = null;
            HttpResponseMessage response = null;

            var task = client_.GetAsync("/api/reviews")
                 .ContinueWith((taskwithresponse) =>
                 {
                     response = taskwithresponse.Result;
                     var jsonString = response.Content.ReadAsStringAsync();
                     jsonString.Wait();
                     models = JsonConvert.DeserializeObject<List<Review>>(jsonString.Result);
                 });
            task.Wait();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(models);
            Assert.Equal(2, models.Count());

            task = client_.GetAsync("/api/reviews/1")
                 .ContinueWith((taskwithresponse) =>
                 {
                     response = taskwithresponse.Result;
                     var jsonString = response.Content.ReadAsStringAsync();
                     jsonString.Wait();
                     model = JsonConvert.DeserializeObject<Review>(jsonString.Result);
                 });
            task.Wait();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(model);
            Assert.Equal(3, model.RestaurantId);

            task = client_.GetAsync("/api/reviews?username=demouser2&minrating=5&restaurantid=2")
                 .ContinueWith((taskwithresponse) =>
                 {
                     response = taskwithresponse.Result;
                     var jsonString = response.Content.ReadAsStringAsync();
                     jsonString.Wait();
                     models = JsonConvert.DeserializeObject<List<Review>>(jsonString.Result);
                 });
            task.Wait();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Single(models);
        }

        [Fact]
        public void CanCreateReviews()
        {
            Review model = null;
            HttpResponseMessage response = null;

            var task = client_.PostAsync("/api/reviews", new StringContent(
                    JsonConvert.SerializeObject(new Review()
                    {
                        UserName = "TestUser",
                        Rating = 1,
                        Description = "Test Description",
                        RestaurantId = 3
                    }).ToString(), Encoding.UTF8, "application/json"))
                .ContinueWith((taskwithresponse) =>
                {
                    response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<Review>(jsonString.Result);
                });
            task.Wait();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(model);

            task = client_.GetAsync("/api/reviews/" + model.ReviewId.ToString())
                .ContinueWith((taskwithresponse) =>
                {
                    response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<Review>(jsonString.Result);
                });
            task.Wait();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(model);
            Assert.Equal("TestUser", model.UserName);

            task = client_.PutAsync("/api/reviews/" + model.ReviewId.ToString(), new StringContent(
                    JsonConvert.SerializeObject(new Review()
                    {
                        UserName = "TestUser",
                        Rating = 1,
                        Description = "Test Description Updated",
                        RestaurantId = 3
                    }).ToString(), Encoding.UTF8, "application/json"))
                .ContinueWith((taskwithresponse) =>
                {
                    response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<Review>(jsonString.Result);
                });
            task.Wait();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(model);
            Assert.Equal("Test Description Updated", model.Description);

            task = client_.GetAsync("/api/reviews/" + model.ReviewId.ToString())
                 .ContinueWith((taskwithresponse) =>
                 {
                     response = taskwithresponse.Result;
                     var jsonString = response.Content.ReadAsStringAsync();
                     jsonString.Wait();
                     model = JsonConvert.DeserializeObject<Review>(jsonString.Result);
                 });
            task.Wait();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(model);
            Assert.Equal("Test Description Updated", model.Description);
        }
    }
}
