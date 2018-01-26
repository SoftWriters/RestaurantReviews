using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using RestaurantReviews.Models;
using RestaurantReviews.ViewModelFactory;

namespace RestaurantReviews.Controllers
{
	public class ReviewController : MenuController
	{
		// GET api/<controller>
		public IEnumerable<string> Get()
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// GET api/<controller>/5
		public string Get(int id)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		public IEnumerable<ReviewViewModel> Get(string user)
		{
			List<Review> reviews = Repository.Value.Reviews
				.Where(rev => rev.User == user && rev.Active)
				.ToList();
			
			ReviewViewModelFactory reviewViewModelFactory = new ReviewViewModelFactory(Request);
			IEnumerable<ReviewViewModel> reviewViewModels = reviewViewModelFactory.MapMany(reviews);

			return reviewViewModels;
		}

		[ShawnsCustomFilters.NonExistingPostActionFilter]
		public CreatedNegotiatedContentResult<ReviewViewModel> Post([FromBody] ReviewViewModel reviewViewModel)
		{
			Restaurant associatedRestaurant = Repository.Value.Restaurants.FirstOrDefault(r => r.Name == reviewViewModel.RestaurantName);
			if (associatedRestaurant == null)
				throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

			//Could also have this in a factory too, but just inline for now
			Review review = new Review
			{
				Score = reviewViewModel.Score,
				Comments = reviewViewModel.Comments,
				Restaurant = associatedRestaurant,
				User = reviewViewModel.User
			};

			Repository.Value.Reviews.Add(review);

			Repository.Value.SaveChanges();

			//Could possibly base class this too
			ReviewViewModelFactory reviewViewModelFactory = new ReviewViewModelFactory(Request);
			ReviewViewModel outputReviewViewModel = reviewViewModelFactory.Map(review);

			return Created(outputReviewViewModel.Href, outputReviewViewModel);
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
			Review review = Repository.Value.Reviews.FirstOrDefault(rest => rest.Id == id == rest.Active);
			if (review == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			review.Active = false;

			Repository.Value.SaveChanges();
		}
	}
}