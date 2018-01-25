using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supr.Model.Entity;
using Supr.Model.Repository;
using Supr.Model.Result;
using System.Linq;

namespace Supr.Test {
	[TestClass]
	public class ModelTests {

		private const string usr = "je.elliott@outlook.com";

		[TestMethod]
		public void ModelTest_GetRestaurantById() {
			// add restaurant to guarantee a find
			Restaurant r = new Restaurant() { Name = "Testaurant", City = "butLEr", State = "PA" };
			using ( SuprContext ctx = new SuprContext() ) {
				ctx.Restaurant.Add( r );
				ctx.SaveChanges();
				int id = r.Id;

				// test the repository
				using ( Repo repo = new Repo() ) {
					ItemResult<Restaurant> restaurant = repo.GetRestaurantById( id );
					Assert.IsNotNull( restaurant, "No restaurant with Id = " + restaurant.Item.Id + " found." );
				}

				// delete the created restaurant
				r = ctx.Restaurant.Where( d => d.Id == id ).First();
				ctx.Restaurant.Remove( r );
				ctx.SaveChanges();
			}
		}

		[TestMethod]
		public void ModelTest_GetRestaurantByCity() {
			// add restaurant to guarantee a find
			Restaurant r = new Restaurant() { Name = "Testaurant", City = "butLEr", State = "PA" };
			using ( SuprContext ctx = new SuprContext() ) {
				ctx.Restaurant.Add( r );
				ctx.SaveChanges();

				// test the repository
				string city = "BUtler";
				using ( Repo repo = new Repo() ) {
					ListResult<Restaurant> restaurants = repo.GetRestaurantsByCity( city );
					Assert.IsNotNull( restaurants, "No restaurants in " + city + " found." );
				}

				// delete the created restaurant
				r = ctx.Restaurant.Where( d => d.Id == r.Id ).First();
				ctx.Restaurant.Remove( r );
				ctx.SaveChanges();
			}
		}

		[TestMethod]
		public void ModelTest_AddRestaurant() {
			// create a new restaurant
			Restaurant r = new Restaurant() { Name = "Testaurant", Category = "Category", City = "Butler", State = "PA", Street = "100 Main Street", ZipCode = "16001" };

			// test model
			using ( Repo repo = new Repo() ) {
				CrudResult result = repo.SaveRestaurant( r );
				Assert.IsTrue( result.Success, "Add restaurant did not succeed." );
			}

			// delete test restaurant
			using ( SuprContext ctx = new SuprContext() ) {
				ctx.Restaurant.Attach( r );
				ctx.Restaurant.Remove( r );
				ctx.SaveChanges();
			}
		}

		[TestMethod]
		public void ModelTest_GetReviewsByUser() {
			// add review to guarantee a find
			Review r = new Review() { RestaurantId = 1, Comments = "This place is supr AWESOME!", Rating = 5, UserId = usr };
			using ( SuprContext ctx = new SuprContext() ) {
				ctx.Review.Add( r );
				ctx.SaveChanges();

				// test the repository
				using ( Repo repo = new Repo() ) {
					ListResult<Review> result = repo.GetReviewsByUser( usr );
					Assert.IsNotNull( result.List, "No review found for user " + usr + "." );
				}

				// delete the created review
				r = ctx.Review.Where( d => d.Id == r.Id ).First();
				ctx.Review.Remove( r );
				ctx.SaveChanges();
			}
		}

		[TestMethod]
		public void ModelTest_AddReview() {
			// create a new review
			Review r = new Review() { RestaurantId = 1, Comments = "This place is supr AWESOME!", Rating = 5, UserId = usr };


			// test model
			using ( Repo repo = new Repo() ) {
				CrudResult result = repo.SaveReview( r );
				Assert.IsTrue( result.Success, "Add review did not succeed." );
			}

			// delete test restaurant
			using ( SuprContext ctx = new SuprContext() ) {
				ctx.Review.Attach( r );
				ctx.Review.Remove( r );
				ctx.SaveChanges();
			}
		}

		[TestMethod]
		public void ModelTest_DeleteReview() {
			// add review to guarantee a find
			Review r = new Review() { RestaurantId = 1, Comments = "This place is supr AWESOME!", Rating = 5, UserId = usr };
			using ( SuprContext ctx = new SuprContext() ) {
				ctx.Review.Add( r );
				ctx.SaveChanges();

				// test the repository
				using ( Repo repo = new Repo() ) {
					CrudResult result = repo.DeleteReview( r.Id );
					Assert.IsTrue( result.Success, "Review did not delete successfully." );
				}
			}
		}
	}
}

