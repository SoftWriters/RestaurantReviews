using RestaurantReviewsAPI.Models;
using DM = RestaurantReviewsAPI.DataModels;
using System.Linq;
using System.Web.Http;
using System;
using System.Net.Http;
using System.Net;

namespace RestaurantReviewsAPI.Controllers
{
	public class StateController : ApiController
	{
		#region GetStates

		[HttpGet]
		public HttpResponseMessage GetStates()
		{
			try
			{
				using (var db = new DM.RestaurantReviewEntities())
				{
					var states = State.GetQueryable(db).ToList();
					return Request.CreateResponse(HttpStatusCode.OK, states);
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	
		#endregion GetStates
	}
}
