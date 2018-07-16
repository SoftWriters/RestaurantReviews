using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RestaurantReviewAPI.Controllers
{
	//This is controllers class which will map the routes for Http Get, Post..
	public class RestaurantController : ApiController
	{
		// GET: api/Restaurant
		[Route("api/Restaurant")]
		public HttpResponseMessage Get()
		{
			var restaurants = Repositories.RestaurantRepository.GetAllRestaurant();
			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, restaurants);
			return response;
		}

		//GET: api/Restaurant/5
		[Route("api/Restaurant/{zipcode:int}")]
		public HttpResponseMessage Get(int zipcode)
		{
			var restaurants = Repositories.RestaurantRepository.SearchRestaurantByZipcode(zipcode);
			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, restaurants);
			return response;
		}

	
		[Route("api/Restaurant/{name:alpha}")]
		public HttpResponseMessage Get(string name)
		{
			var restaurants = Repositories.RestaurantRepository.SearchRestaurantByName(name);
			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, restaurants);
			return response;
		}

		// POST: api/Restaurant
		[Route("api/Restaurant")]
		public HttpResponseMessage Post(Restaurant res)
        {
			
			Repositories.RestaurantRepository.InsertRestaurant(res);
			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
			return response;
        }
		// DELETE: api/Restaurant/5
		[Route("api/Restaurant/{id}")]
		public HttpResponseMessage Delete(int id)
		{
			Repositories.RestaurantRepository.DeleteRestaurant(id);
			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
			return response;

			
		}

		//// GET: api/Restaurant/name
		//[Route("api/Restaurant/{city:alpha}")]
		//public HttpResponseMessage Get(string city)
		//{
		//	var restaurants = Repositories.RestaurantRepository.SearchRestaurantByCity(city);
		//	HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, restaurants);
		//	return response;
		//}

		//[HttpGet]
		//[ActionName("GetName")]

	}
}
