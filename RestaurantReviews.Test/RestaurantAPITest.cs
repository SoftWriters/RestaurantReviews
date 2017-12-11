using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using RestaurantReviews.Models;
using System.Linq;

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
        public async Task CanGetAllRestaurants()
        {
            // Act
            List<Restaurant> model = null;
            HttpResponseMessage response = null;

            var task = client_.GetAsync("/api/restaurants")
                .ContinueWith((taskwithresponse) =>
                {
                    response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<Restaurant>>(jsonString.Result);
                });
            task.Wait();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.True(model.Count() == 3);
        }
    }
}
