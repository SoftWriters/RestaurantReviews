using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Supr.Model.Entity;
using Supr.Model.Result;
using System.Collections.Generic;
using System.Linq;

namespace Supr.Test {
	[TestClass]
	public class ApiTests {

		private const string usr = "je.elliott@outlook.com";
		private const string pwd = "pa$$W0rd";
		private const string uri = "http://localhost:8500";

		[TestMethod]
		public void ApiTest_GetRestaurantById() {
			// add restaurant to guarantee a find
			Restaurant r = new Restaurant() { Name = "Testaurant", City = "butLEr", State = "PA" };
			using ( SuprContext ctx = new SuprContext() ) {
				ctx.Restaurant.Add( r );
				ctx.SaveChanges();
				int id = r.Id;

				// test the api
				var token = ApiClient.GetAPIToken( usr, pwd, uri ).Result;
				var response = ApiClient.GetRequest( token, uri, "/api/v1/GetRestaurant?Id=" + id ).Result;

				ItemResult<Restaurant> result = JsonConvert.DeserializeObject<ItemResult<Restaurant>>( response.ToString() );
				Assert.IsNotNull( result, "No restaurant with Id = " + result.Item.Id + " found." );

				// delete the created restaurant
				r = ctx.Restaurant.Where( d => d.Id == id ).First();
				ctx.Restaurant.Remove( r );
				ctx.SaveChanges();
			}
		}

		[TestMethod]
		public void ApiTest_GetRestaurantByCity() {
			// add restaurant to guarantee a find
			Restaurant r = new Restaurant() { Name = "Testaurant", City = "butLEr", State = "PA" };
			using ( SuprContext ctx = new SuprContext() ) {
				ctx.Restaurant.Add( r );
				ctx.SaveChanges();
				string city = "BUtler";

				// test the api
				var token = ApiClient.GetAPIToken( usr, pwd, uri ).Result;
				var response = ApiClient.GetRequest( token, uri, "/api/v1/GetRestaurant?City=" + city ).Result;

				ListResult<Restaurant> result = JsonConvert.DeserializeObject<ListResult<Restaurant>>( response.ToString() );
				Assert.IsNotNull( result, "No restaurants were found in " + city + "." );

				// delete the created restaurant
				r = ctx.Restaurant.Where( d => d.Id == r.Id ).First();
				ctx.Restaurant.Remove( r );
				ctx.SaveChanges();
			}
		}

		[TestMethod]
		public void ApiTest_AddRestaurant() {
			// create a new restaurant
			KeyValuePair<string, string> kvp = new KeyValuePair<string, string>();
			List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();
			kvp = new KeyValuePair<string, string>( "Name", "Testaurant" );
			formData.Add( kvp );
			kvp = new KeyValuePair<string, string>( "Category", "Category" );
			formData.Add( kvp );
			kvp = new KeyValuePair<string, string>( "City", "Butler" );
			formData.Add( kvp );
			kvp = new KeyValuePair<string, string>( "State", "PA" );
			formData.Add( kvp );
			kvp = new KeyValuePair<string, string>( "Street", "100 Main Street" );
			formData.Add( kvp );
			kvp = new KeyValuePair<string, string>( "ZipCode", "16001" );
			formData.Add( kvp );

			// test api
			var token = ApiClient.GetAPIToken( usr, pwd, uri ).Result;
			var response = ApiClient.PostRequest( token, uri, "/api/v1/AddRestaurant", formData ).Result;

			CrudResult result = JsonConvert.DeserializeObject<CrudResult>( response.ToString() );
			Assert.IsTrue( result.Success, "Add restaurant did not succeed." );

			// delete test restaurant
			using ( SuprContext ctx = new SuprContext() ) {
				Restaurant r = ctx.Restaurant.Where( d => d.Name == "Testaurant" ).First();
				ctx.Restaurant.Remove( r );
				ctx.SaveChanges();
			}
		}

		[TestMethod]
		public void ApiTest_GetReviewsByUser() {
			// add review to guarantee a find
			Review r = new Review() { RestaurantId = 1, Comments = "This place is supr AWESOME!", Rating = 5, UserId = usr };
			using ( SuprContext ctx = new SuprContext() ) {
				ctx.Review.Add( r );
				ctx.SaveChanges();

				// test the api
				var token = ApiClient.GetAPIToken( usr, pwd, uri ).Result;
				var response = ApiClient.GetRequest( token, uri, "/api/v1/GetReviews?User=" + usr ).Result;

				ListResult<Review> result = JsonConvert.DeserializeObject<ListResult<Review>>( response.ToString() );
				Assert.IsNotNull( result, "No reviews were found for " + usr + "." );

				// delete the created review
				r = ctx.Review.Where( d => d.Id == r.Id ).First();
				ctx.Review.Remove( r );
				ctx.SaveChanges();
			}
		}

		[TestMethod]
		public void ApiTest_AddReview() {
			// create a new review
			KeyValuePair<string, string> kvp = new KeyValuePair<string, string>();
			List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();
			kvp = new KeyValuePair<string, string>( "RestaurantId", "1" );
			formData.Add( kvp );
			kvp = new KeyValuePair<string, string>( "Comments", "This place is supr AWESOME!" );
			formData.Add( kvp );
			kvp = new KeyValuePair<string, string>( "Rating", "5" );
			formData.Add( kvp );
			kvp = new KeyValuePair<string, string>( "UserId", "je.elliott@outlook.com" );
			formData.Add( kvp );

			// test the api
			var token = ApiClient.GetAPIToken( usr, pwd, uri ).Result;
			var response = ApiClient.PostRequest( token, uri, "/api/v1/AddReview", formData ).Result;

			CrudResult result = JsonConvert.DeserializeObject<CrudResult>( response.ToString() );
			Assert.IsTrue( result.Success, "Add review did not succeed." );

			// delete test review
			using ( SuprContext ctx = new SuprContext() ) {
				Review r = ctx.Review.Where( d => d.RestaurantId == 1 && d.UserId == "je.elliott@outlook.com" ).First();
				ctx.Review.Remove( r );
				ctx.SaveChanges();
			}
		}
		[TestMethod]
		public void ApiTest_DeleteReview() {
			// add review to guarantee a find
			Review r = new Review() { RestaurantId = 1, Comments = "This place is supr AWESOME!", Rating = 5, UserId = usr };
			using ( SuprContext ctx = new SuprContext() ) {
				ctx.Review.Add( r );
				ctx.SaveChanges();

				// test the api
				var token = ApiClient.GetAPIToken( usr, pwd, uri ).Result;
				var response = ApiClient.DeleteRequest( token, uri, "/api/v1/DeleteReview?Id=" + r.Id ).Result;

				CrudResult result = JsonConvert.DeserializeObject<CrudResult>( response.ToString() );
				Assert.IsTrue( result.Success, "Delete review did not succeed." );
			}
		}
	}
}
