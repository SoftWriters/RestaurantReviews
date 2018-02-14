using RestaurantReviewsAPI.Models;
using DM = RestaurantReviewsAPI.DataModels;
using System.Linq;
using System.Web.Http;
using System;
using System.Net.Http;
using System.Net;

namespace RestaurantReviewsAPI.Controllers
{
	public class ReviewController : ApiController
	{
		#region GetReviewsByUser

		[HttpGet]
		public HttpResponseMessage GetReviewsByUser(string username)
		{
			try
			{
				using (var db = new DM.RestaurantReviewEntities())
				{
					var reviews = Review.GetQueryable(db).Where(rv => rv.Reviewer.Username == username).ToList();
					return Request.CreateResponse(HttpStatusCode.OK, reviews);
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		#endregion GetReviewsByUser

		#region SaveReview

		[HttpPost]
		public HttpResponseMessage SaveReview(Review review)
		{
			try
			{
				using (var db = new DM.RestaurantReviewEntities())
				{
					var reviewDb = review.Save(db);
					db.SaveChanges();
					return Request.CreateResponse(HttpStatusCode.OK, reviewDb?.Id ?? 0);
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		#endregion SaveReview

		#region DeleteReview

		[HttpDelete]
		public HttpResponseMessage DeleteReview(int id)
		{
			try
			{
				using (var db = new DM.RestaurantReviewEntities())
				{
					var review = db.Reviews.SingleOrDefault(rv => rv.Id == id);
					if (review == null) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Review Not Found");
					db.Reviews.Remove(review);
					db.SaveChanges();
					return Request.CreateResponse(HttpStatusCode.OK, true);
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		#endregion DeleteReview
	}
}
