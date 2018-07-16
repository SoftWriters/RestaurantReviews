using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient
{   //This Class will call API methods from RestaurantAPICalls, ReviewAPICalls and has methods to display output on the screen.
	public class ExecuteMethods
	{
		RestaurantAPICalls callRestaurantApi = new RestaurantAPICalls();
		ReviewAPICalls callReviewApi = new ReviewAPICalls();
		public void GetAllRestaurants()
		{			
			var restaurants = callRestaurantApi.GetRestaurants();
			DisplayRestaurants(restaurants);
		}

		public void GetRestaurantByZipCode()
		{			
			var restaurants = callRestaurantApi.GetRestaurantByZipcode(15143);
			DisplayRestaurants(restaurants);
		}

		public void AddRestaurant()
		{
			var restaurant = callRestaurantApi.AddRestaurant(new Entities.Restaurant { Name = "XYZ", Address = "XYZ", Zipcode = 15143, City="Sewickley" });
			DisplayRestaurants(restaurant);
		}
		public void DeleteRestaurant()
		{
			var restaurants = callRestaurantApi.DeleteRestaurant(new Entities.Restaurant { Name = "XYZ", Address = "XYZ", Zipcode = 15143, City="Sewickley" });
			DisplayRestaurants(restaurants);
		}
		public void GetAllReviews()
		{
			var reviews = callReviewApi.GetReviews();
			DisplayReviews(reviews);

		}
		public void GetReviewByUser()
		{
			var reviews = callReviewApi.GetReviewByUser(301);
			DisplayReviews(reviews);
		}
		public void AddReview()
		{
			var reviews = callReviewApi.AddReview(new Entities.Reviews {UserId = 501, RestaurantId = 1001, Comment = "XYZ",Stars = 1 });
			DisplayReviews(reviews);
		}

		public void DeleteReview()
		{
			var reviews = callReviewApi.DeleteReview(new Entities.Reviews { UserId = 501, RestaurantId = 1001, Comment = "XYZ", Stars = 1 });
			DisplayReviews(reviews);
		}

		public void DisplayRestaurants(object obj)
		{
			var restaurants = (List<Entities.Restaurant>)obj;
			Console.WriteLine("----------------------------------------------------------------------------");
			Console.WriteLine("RestaurantId    ZipCode          Restaurant Name");
			Console.WriteLine("----------------------------------------------------------------------------");
			foreach (Entities.Restaurant rest in restaurants)
			{
				Console.WriteLine(String.Format("{0,-15}  {1,-15}  {2,-15}",rest.RestaurantId.ToString(),rest.Zipcode,rest.Name));
			}
			Console.WriteLine("----------------------------------------------------------------------------");
		}

		public void DisplayReviews(object obj)
		{
			var reviews = (List<Entities.RestaurantReview>)obj;
			Console.WriteLine("----------------------------------------------------------------------------");
			Console.WriteLine("Restaurant       Review              Stars");
			Console.WriteLine("----------------------------------------------------------------------------");		
			foreach (var rev in reviews)
			{
				Console.WriteLine(String.Format("{0,-15}  {1,-15}  {2,5}", rev.RestaurantName, rev.Comments, rev.Ratings));
				
			}
			Console.WriteLine("----------------------------------------------------------------------------");
		}
	}
}
