using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RestaurantReviews.Api.Model;
using RestaurantReviews.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using User = RestaurantReviews.Api.Model.User;

namespace Test
{
    /// <summary>
    /// Program
    /// 
    ///     This program tests the 3 API's.
    /// 
    /// </summary>
    public class Program
    {
        public static void Main(string[] args) 
        {            
            using (var factory = new WebApplicationFactory<RestaurantReviews.Startup>())
            {               
                using (var client = factory.CreateClient())
                {
                    Test_Restaurant_Api(client).Wait();
                    Test_Review_Api(client).Wait();
                    Test_User_Api(client).Wait();
                }
            }

            System.Environment.Exit(0);
        }

        /// <summary>
        /// Test_Restaurant
        ///   
        ///     Test the Restaurant API
        /// </summary>
        /// <param name="client">The client</param>
        private static async Task Test_Restaurant_Api(HttpClient client)
        {
            try
            {   //
                // Test: Get a list of Restaurants by City
                //
                var response = await client.GetAsync("/restaurant/list/Pittsburgh");
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.OK);
                string data = await response.Content.ReadAsStringAsync();
                List<Restaurant> restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(data);
                Assert.Equal(restaurants.Count, (int) 3);
                //
                // Test: Post a restaurant not in the db (in-memory DB)
                //
                string restaurantParms = "AppleBees/999 State Street/Pittsburgh/PA/US/15222";
                response = await client.PostAsync("/restaurant/create/" + restaurantParms, null);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.Created);
                data = await response.Content.ReadAsStringAsync();
                Restaurant restaurant = JsonConvert.DeserializeObject<Restaurant>(data);
                //
                // Test: Get a list of Restaurants by City (updated - 1 more in Pittsburgh)
                //
                response = await client.GetAsync("/restaurant/list/Pittsburgh");
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.OK);
                data = await response.Content.ReadAsStringAsync();
                restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(data);
                Assert.Equal(restaurants.Count, (int)4);
                //
                // Test: Delete a Restaurant
                //                
                response = await client.DeleteAsync("/restaurant/delete/" + restaurant.RestaurantId);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.OK);
                //
                // Test: Get a list of Restaurants by City
                //
                response = await client.GetAsync("/restaurant/list/Pittsburgh");
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.OK);
                data = await response.Content.ReadAsStringAsync();
                restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(data);
                Assert.Equal(restaurants.Count, (int)3);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Restaurant Test Failed due to Error = [{ex.Message}] - [{ex.StackTrace}]");
            }
        }

        /// <summary>
        /// Test_Reviews
        ///   
        ///     Test the Review API
        /// </summary>
        /// <param name="client">The client</param>
        private static async Task Test_Review_Api(HttpClient client)
        {
            try
            {   //
                // Test: Add a Review
                //
                string userParms = "Steven/4127151234";                
                var response = await client.PostAsync("/user/create/" + userParms, null);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.Created);
                string data = await response.Content.ReadAsStringAsync();
                User user = JsonConvert.DeserializeObject<User>(data);

                Restaurant restaurant = new Restaurant
                {
                    Name = "Chik Fila",
                    StreetAddress = "888 State Street",
                    City = "Pittsburgh",
                    Region = "PA",
                    Country = "US",
                    PostalCode = "15122"
                };
                                
                string json = JsonConvert.SerializeObject(restaurant);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync("/restaurant/add", httpContent);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.Created);
                data = await response.Content.ReadAsStringAsync();
                restaurant = JsonConvert.DeserializeObject<Restaurant>(data);

                Review review = new Review(
                    DateTime.Now,
                    restaurant.RestaurantId,
                    user.UserId,
                    Rating.FourStar,
                    "Test Review"
                );

                json = JsonConvert.SerializeObject(review);
                httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync("/review/add", httpContent);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.Created);
                data = await response.Content.ReadAsStringAsync();
                review = JsonConvert.DeserializeObject<Review>(data);
                //
                // Test: Get a list of Reviews by User
                //                
                response = await client.GetAsync("/review/user/" + user.UserId);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.OK);
                data = await response.Content.ReadAsStringAsync();
                List<Review> reviews = JsonConvert.DeserializeObject<List<Review>>(data);
                Assert.Equal(reviews.Count, (int)8);
                //
                // Test: Delete a Review
                //                
                response = await client.DeleteAsync("/review/delete/" + review.ReviewId);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.OK);
                //
                // Test: Get a list of Reviews by User
                //                
                response = await client.GetAsync("/review/user/" + user.UserId);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.OK);
                data = await response.Content.ReadAsStringAsync();
                reviews = JsonConvert.DeserializeObject<List<Review>>(data);
                Assert.Equal(reviews.Count, (int)7);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Review Test Failed due to Error = [{ex.Message}] - [{ex.StackTrace}]");
            }
        }

        /// <summary>
        /// Test_User
        ///   
        ///     Test the User API
        /// </summary>
        /// <param name="client">The client</param>
        private static async Task Test_User_Api(HttpClient client)
        {
            try
            {   //
                // Test: Add a User
                //                
                var response = await client.PostAsync("/user/create/Fred/7241112222", null);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.Created);
                string data = await response.Content.ReadAsStringAsync();
                User user = JsonConvert.DeserializeObject<User>(data);
                Assert.Equal("Fred", user.Name);
                //
                // Test: Get a User by UserId
                //                
                response = await client.GetAsync("/user/" + user.UserId);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.OK);
                data = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(data);
                Assert.Equal("Fred", user.Name);
                //
                // Test: Delete a User
                //                
                response = await client.DeleteAsync("/user/delete/" + user.UserId);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.OK);
                //
                // Test: Get a User by UserId
                //                
                response = await client.GetAsync("/user/" + user.UserId);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.NotFound);
                //
                // Test: Add a User
                //               
                response = await client.PostAsync("/user/create/William/7249998888", null);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.Created);
                data = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(data);
                //
                // Test: Delete a User
                //                
                response = await client.DeleteAsync("/user/delete/" + user.UserId);
                Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"User Test Failed due to Error = [{ex.Message}] - [{ex.StackTrace}]");
            }
        }
    }
}
