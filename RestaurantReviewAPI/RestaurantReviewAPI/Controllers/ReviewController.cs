using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantReviewAPI.Controllers
{
	//This is controllers class which will map the routes for Http Get, Post..
	public class ReviewController : ApiController
	{
		// GET: api/Restaurant
		[Route("api/Reviews")]
		public HttpResponseMessage Get()
		{
			var reviews = Repositories.ReviewRepository.GetAllReviews();
			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, reviews);
			return response;
		}

		//GET: api/Restaurant/5
		[Route("api/Reviews/{userid?}")]
		public HttpResponseMessage Get(int userid)
		{
			var reviews = Repositories.ReviewRepository.GetReviewByUser(userid);
			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, reviews);
			return response;
		}

		// POST: api/Review
		[Route("api/Reviews")]
		public HttpResponseMessage Post(Review rev)
		{
			Repositories.ReviewRepository.InsertReview(rev);		
			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
			return response;

		}

		// DELETE: api/Review/5
		[Route("api/Reviews/{id}")]
		public HttpResponseMessage Delete(int id)
		{
		    Repositories.ReviewRepository.DeleteReview(id);
			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
			return response;
		}
	}
}
