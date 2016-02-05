using Newtonsoft.Json;
using Supr.Model.Entity;
using Supr.Model.Repository;
using Supr.Model.Result;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Supr.Api.controllers {
	[Authorize]
	public class V1Controller : ApiController {

		#region Setup
		// this could be set globally, but my first choice is to keep it as close to the problem as possible so it doesn't get forgotten
		// ignore EF circular references
		JsonSerializerSettings jss = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
		#endregion

		#region Restaurant
		[HttpGet]
		public HttpResponseMessage GetRestaurant( string city ) {
			using ( Repo repo = new Repo() ) {
				ListResult<Restaurant> result = repo.GetRestaurantsByCity( city );
				string json = JsonConvert.SerializeObject( result, jss );
				HttpResponseMessage msg = Request.CreateResponse( HttpStatusCode.OK, json );
				return msg;
			}
		}
		[HttpGet]
		public HttpResponseMessage GetRestaurant( int id ) {
			using ( Repo repo = new Repo() ) {
				ItemResult<Restaurant> result = repo.GetRestaurantById( id );
				string json = JsonConvert.SerializeObject( result, jss );
				HttpResponseMessage msg = Request.CreateResponse( HttpStatusCode.OK, json );
				return msg;
			}
		}
		[HttpPost]
		public HttpResponseMessage AddRestaurant( Restaurant restaurant ) {
			CrudResult result = new CrudResult();
			using ( Repo repo = new Repo() ) {
				result = repo.SaveRestaurant( restaurant );
				string json = JsonConvert.SerializeObject( result, jss );
				HttpResponseMessage msg = Request.CreateResponse( HttpStatusCode.OK, json );
				return msg;
			}
		}
		#endregion

		#region Review
		[HttpGet]
		public HttpResponseMessage GetReview( string user ) {
			using ( Repo repo = new Repo() ) {
				ListResult<Review> result = repo.GetReviewsByUser( user );
				string json = JsonConvert.SerializeObject( result, jss );
				HttpResponseMessage msg = Request.CreateResponse( HttpStatusCode.OK, json );
				return msg;
			}
		}
		[HttpPost]
		public HttpResponseMessage AddReview( Review review ) {
			CrudResult result = new CrudResult();
			using ( Repo repo = new Repo() ) {
				result = repo.SaveReview( review );
				string json = JsonConvert.SerializeObject( result, jss );
				HttpResponseMessage msg = Request.CreateResponse( HttpStatusCode.OK, json );
				return msg;
			}
		}
		[HttpDelete]
		public HttpResponseMessage DeleteReview( int id ) {
			CrudResult result = new CrudResult();
			using ( Repo repo = new Repo() ) {
				result = repo.DeleteReview( id );
				string json = JsonConvert.SerializeObject( result, jss );
				HttpResponseMessage msg = Request.CreateResponse( HttpStatusCode.OK, json );
				return msg;
			}
		}
		#endregion

		#region Debug
		// anonymous method for basic uri hit testing
#if DEBUG
		[AllowAnonymous, HttpGet]
		public DateTime WhatTimeIsIt() {
			return DateTime.Now;
		}
#endif
		#endregion
	}
}
