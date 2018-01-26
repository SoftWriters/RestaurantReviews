using System;
using System.Web.Http;

namespace RestaurantReviews.Controllers
{
	public class MenuController : ApiController
	{
		//Keep these in one place and lazy as needed - no problem seen at first but could be down the road...
		//....write once, refactor...
		public Lazy<RestaurantReviewDatabaseEntities> Repository = new Lazy<RestaurantReviewDatabaseEntities>();
	}
}