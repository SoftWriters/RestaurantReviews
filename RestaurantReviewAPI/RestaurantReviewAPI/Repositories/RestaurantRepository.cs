using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviewAPI.Repositories
{   //This class has linq commands to get data(Restaurant Table) from the database.
	public class RestaurantRepository
	{

		private static RestaurantReviewEntities dataContext = new RestaurantReviewEntities();
		public static List<Restaurant> GetAllRestaurant()
		{
				var query = from Restaurant in dataContext.Restaurants select Restaurant;
				return query.ToList();		
		}

		public static List<Restaurant> SearchRestaurantByZipcode(int zipcode)
		{
					
				var query = from Restaurant in dataContext.Restaurants where Restaurant.Zipcode == zipcode select Restaurant;
				return query.ToList();
			
		}

		public static List<Restaurant> SearchRestaurantByCity(string city)
		{
			
				var query = from Restaurant in dataContext.Restaurants where Restaurant.City == city select Restaurant;
				return query.ToList();
			
		}

		public static List<Restaurant> SearchRestaurantByName(string name)
		{
			

				var query = from Restaurant in dataContext.Restaurants where Restaurant.Name == name select Restaurant;
				return query.ToList();
			
		}
		public static void  InsertRestaurant(Restaurant res)
		{
			var query = (from Restaurant in dataContext.Restaurants
						 where Restaurant.Name == res.Name where Restaurant.Address == res.Address where Restaurant.City == res.City
						 select Restaurant).SingleOrDefault();
			if (query == null)
			{
				dataContext.Restaurants.Add(res);
				dataContext.SaveChanges();
			}
		}

		public static void DeleteRestaurant(int id)
		{
				var query = (from Restaurant in dataContext.Restaurants
							 where Restaurant.RestaurantId == id
							 select Restaurant).SingleOrDefault();
				dataContext.Restaurants.Remove(query);
				dataContext.SaveChanges();
			
		}
	}
}