using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WebClient
{
	//This class will call method of user defined Http class
	public class RestaurantAPICalls
	{
		private Http _http;

		public RestaurantAPICalls()
		{
			_http = new Http(ConfigurationManager.AppSettings["URL"]);
		}

		public List<Entities.Restaurant> GetRestaurants()
		{
			return _http.Get<List<Entities.Restaurant>>("api/Restaurant");

		}
		public List<Entities.Restaurant> GetRestaurantByZipcode( int zipcode)
		{
			return _http.Get<List<Entities.Restaurant>>("api/Restaurant/"+zipcode);

		}
		public List<Entities.Restaurant> AddRestaurant(Entities.Restaurant rest)
		{
			
			_http.Post<Entities.Restaurant>("api/Restaurant", rest);
			return _http.Get<List<Entities.Restaurant>>("api/Restaurant");

		}
		public List<Entities.Restaurant> DeleteRestaurant(Entities.Restaurant rest)
		{
			var restget = _http.Get<List<Entities.Restaurant>>("api/Restaurant/" + rest.Name);
			_http.Delete<List<Entities.Restaurant>>("api/Restaurant", restget[0].RestaurantId);
			return _http.Get<List<Entities.Restaurant>>("api/Restaurant");
		}

	}
}
