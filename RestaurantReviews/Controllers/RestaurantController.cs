using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using RestaurantReviews.Models;
using RestaurantReviews.ViewModelFactory;

namespace RestaurantReviews.Controllers
{
    public class RestaurantController : MenuController
    {
	    // GET: api/Restaurant
		public IEnumerable<string> Get()
        {
	        return null;
        }

		[HttpGet]
	    public IEnumerable<RestaurantViewModel> Get(string city)
		{
			//Only in instances when dealing with Linq or queries such as this to I like opt for var, if the team is okay with it
			var restaurants = from restaurant 
					   in Repository.Value.Restaurants
					   where restaurant.City == city 
					   select restaurant;

			//End it before it gets too far if nothing found
			if (restaurants.Count() == 0)
				//Throw the exception so we don't have to complicate the return type and the resultant code
				throw new HttpResponseException(HttpStatusCode.NotFound);

			RestaurantViewModelFactory restaurantViewModelFactory = new RestaurantViewModelFactory(Request);
			//Btw, I use Resharper Alt + Enter so I don't have to think about typing the type out. Very helpful.
			IEnumerable<RestaurantViewModel> restaurantViewModels = restaurantViewModelFactory.MapMany(restaurants);

			return restaurantViewModels;
		}

        // GET: api/Restaurant/5
        public string Get(int id)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        [ShawnsCustomFilters.NonExistingPostActionFilter]
        public CreatedNegotiatedContentResult<RestaurantViewModel> Post([FromBody]RestaurantViewModel restaurantViewModel)
        {
			//Don't accept a post with an id
			if (restaurantViewModel.Id.HasValue)
				throw new HttpResponseException(HttpStatusCode.BadRequest);

			//Variabalizing operations is huge in terms of developer context. See 'Clean Code' by Martin, 'Code Complete' by McConnell
	        bool nameAlreadyExists = Repository.Value.Restaurants.Any(r => r.Name == restaurantViewModel.Name);
			if (nameAlreadyExists)
				throw new HttpResponseException(HttpStatusCode.Conflict);

			//Could also have this in a factory too, but just inline for now
			Restaurant restaurant = new Restaurant
			{
				Name = restaurantViewModel.Name,
				City = restaurantViewModel.City
			};

	        Repository.Value.Restaurants.Add(restaurant);

	        Repository.Value.SaveChanges();

	        RestaurantViewModelFactory restaurantViewModelFactory = new RestaurantViewModelFactory(Request);
	        RestaurantViewModel outputRestaurantViewModel = restaurantViewModelFactory.Map(restaurant);

			return Created(outputRestaurantViewModel.Href, outputRestaurantViewModel);
        }

        // PUT: api/Restaurant/5
        public void Put(int id, [FromBody]string value)
        {
	        throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

        // DELETE: api/Restaurant/5
        public void Delete(int id)
        {
	        throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}
    }
}
