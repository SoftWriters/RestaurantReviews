using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WebClient
{
	//This class will call method of user defined Http class
	public class ReviewAPICalls
	{
		private Http _http;

		public ReviewAPICalls()
		{
			_http = new Http(ConfigurationManager.AppSettings["URL"]);
		}

		//public GetReviews()
		//{List<Entities.Reviews> 
		//	return _http.Get<List<Entities.Reviews>>("api/Reviews");

		//}

		public List<Entities.RestaurantReview> GetReviews()
		{
			return _http.Get<List<Entities.RestaurantReview>>("api/Reviews");

		}

		public List<Entities.RestaurantReview> GetReviewByUser(int userid)
		{
			return _http.Get<List<Entities.RestaurantReview>>("api/Reviews"+"/"+userid);

		}

		public List<Entities.RestaurantReview> AddReview(Entities.Reviews rev)
		{
			
			_http.Post<Entities.Reviews>("api/Reviews", rev);
			return _http.Get<List<Entities.RestaurantReview>>("api/Reviews");
			

		}
		public List<Entities.RestaurantReview> DeleteReview(Entities.Reviews rev)
		{
			var revget = _http.Get<List<Entities.RestaurantReview>>("api/Reviews/" + rev.UserId);
			_http.Delete<List<Entities.Reviews>>("api/Reviews", revget[0].RevId);
			return _http.Get<List<Entities.RestaurantReview>>("api/Reviews");
		}

	}
}
