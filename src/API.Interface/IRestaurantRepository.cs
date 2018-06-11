/******************************************************************************
 * Name: IRestaurantRepository.cs
 * Purpose: Restaurant Repository Interface that implements methods for 
 *           Restaurant Model
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
    public interface IRestaurantRepository : IAPIDataRepository
    {
        RestaurantModelList GetRestaurants(string city);
        RestaurantModelDTO GetRestaurantById(int id);
        bool CheckRestaurantExists(RestaurantModelDTO restaurant);
        RestaurantModelDTO AddRestaurant(RestaurantModelDTO newRestaurant);
    }
}
