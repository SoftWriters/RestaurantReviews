/******************************************************************************
 * Name: IRestaurantService.cs
 * Purpose: Restaurant Service Interface that implements methods for Restaurant
 *           Model
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using RestaurantReviews.API.Model.DTO;

namespace RestaurantReviews.API.Interface
{
    public interface IRestaurantService
    {
        RestaurantModelList GetRestaurants(string city);
        RestaurantModelDTO GetRestaurantById(int id);
        RestaurantModelDTO AddRestaurant(RestaurantModelDTO newRestaurant);
    }
}
