using System.Collections.Generic;
using System.Net.Http;
using RestaurantReviews.Controllers;
using RestaurantReviews.Models;

namespace RestaurantReviews.ViewModelFactory
{
	public class ReviewViewModelFactory : ViewModelFactory<Review, ReviewViewModel, ReviewController>
	{
		//I'm a fan of making thin constructors or methods when no logic. Tightens up the class a bit.
		public ReviewViewModelFactory(HttpRequestMessage httpRequestMessage) : base(httpRequestMessage) { }

		public override ReviewViewModel Map(Review review)
		{
			ReviewViewModel viewModel = CoreMappingLogic(review);
			return viewModel;
		}

		protected override void MappingLogic(Review model, ReviewViewModel resultantViewModelType)
		{
			//Could have used automapper for items such as this to take care of same named properties - huge worksaver - didn't in this case.
			resultantViewModelType.Id = model.Id;
			resultantViewModelType.Score = model.Score;
			resultantViewModelType.RestaurantName = model.Restaurant.Name;
			resultantViewModelType.Comments = model.Comments;
			resultantViewModelType.User = model.User;
		}
	}
}