using RestaurantReviewsAPI.Models;
using DM = RestaurantReviewsAPI.DataModels;
using System.Linq;
using System.Web.Http;
using System;
using System.Net.Http;
using System.Net;

namespace RestaurantReviewsAPI.Controllers
{
	public class RestaurantController : ApiController
	{
		#region GetRestaurants

		[HttpGet]
		public HttpResponseMessage GetRestaurants()
		{
			try
			{
				using (var db = new DM.RestaurantReviewEntities())
				{
					var restaurants = Restaurant.GetQueryable(db).ToList();
					return Request.CreateResponse(HttpStatusCode.OK, restaurants);
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		#endregion GetRestaurants

		#region GetRestaurantsByCity

		[HttpGet]
		public HttpResponseMessage GetRestaurantsByCity(string city, int stateId)
		{
			try
			{
				using (var db = new DM.RestaurantReviewEntities())
				{
					var restaurants = Restaurant.GetQueryable(db).Where(r => r.City == city && r.State.Id == stateId).ToList();
					return Request.CreateResponse(HttpStatusCode.OK, restaurants);
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		#endregion GetRestaurantsByCity

		#region SaveRestaurant

		[HttpPost]
		public HttpResponseMessage SaveRestaurant(Restaurant restaurant)
		{
			try
			{
				using (var db = new DM.RestaurantReviewEntities())
				{
					var restaurantDb = restaurant.Save(db);
					db.SaveChanges();
					return Request.CreateResponse(HttpStatusCode.OK, restaurantDb?.Id ?? 0);
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		#endregion SaveRestaurant
	}
}
