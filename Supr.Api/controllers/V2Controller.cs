using Newtonsoft.Json;
using Supr.Model.Entity;
using Supr.Model.Repository;
using Supr.Model.Result;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Supr.Api.controllers {
	// I would create new Vn controllers to allow the API to move forward without breaking existing functionality in any existing api's
	[Authorize]
	public class V2Controller : ApiController {
		#region Setup
		// this could be set globally, but my first choice is to keep it as close to the problem as possible so it doesn't get forgotten
		// ignore EF circular references
		JsonSerializerSettings jss = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
		#endregion

		#region Restaurant
		[HttpGet]
		public HttpResponseMessage GetRestaurant( string city, string category ) {
			using( Repo repo = new Repo() ) {
				ListResult<Restaurant> result = repo.GetRestaurantsByCity( city, category );

				string json = JsonConvert.SerializeObject( result, jss );

				HttpResponseMessage msg = Request.CreateResponse( HttpStatusCode.OK, json );
				return msg;
			}
		}
		#endregion

		// and so on
		// ... 

	}
}
