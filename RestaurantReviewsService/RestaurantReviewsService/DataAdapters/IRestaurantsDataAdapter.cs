using RestaurantReviewsService.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsService.DataAdapters
{
    public interface IRestaurantsDataAdapter
    {
        IList<RestaurantDM> GetAllRestaurants();

        IList<RestaurantDM> GetRestaurantsByCity(string cityName);

        int AddNewRestaurant(RestaurantDM restaurant);
    }
}
