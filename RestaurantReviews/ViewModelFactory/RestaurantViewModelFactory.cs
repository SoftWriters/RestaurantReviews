using System.Collections.Generic;
using System.Net.Http;
using RestaurantReviews.Controllers;
using RestaurantReviews.Models;

namespace RestaurantReviews.ViewModelFactory
{
	public class RestaurantViewModelFactory : ViewModelFactory<Restaurant, RestaurantViewModel, RestaurantController>
	{
		//I'm a fan of making thin constructors or methods when no logic. Tightens up the class a bit.
		public RestaurantViewModelFactory(HttpRequestMessage httpRequestMessage) : base(httpRequestMessage) { }

		public override RestaurantViewModel Map(Restaurant restaurant)
		{
			RestaurantViewModel viewModel = CoreMappingLogic(restaurant);
			return viewModel;
		}

		protected override void MappingLogic(Restaurant model, RestaurantViewModel resultantViewModelType)
		{
			//Could have used automapper for items such as this to take care of same named properties - huge worksaver - didn't in this case.
			resultantViewModelType.Id = model.Id;
			resultantViewModelType.Name = model.Name;
			resultantViewModelType.City = model.City;
		}
	}
}