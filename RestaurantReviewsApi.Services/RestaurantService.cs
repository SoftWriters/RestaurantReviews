using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantReviews.Data.Interfaces;
using RestaurantReviews.Model;

namespace RestaurantReviews.Services
{
    public interface IRestaurantService
    {
        List<Restaurant> GetAllRestaurants(string name = null);
        List<Restaurant> GetRestaurantsByCity(string city);
        Restaurant GetRestaurantById(Guid id);
        void CreateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);
        void UpdateRestaurant(Restaurant restaurant);
        void SaveChanges();
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A service for accessing restaurants information. </summary>
    ///-------------------------------------------------------------------------------------------------

    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        ///-------------------------------------------------------------------------------------------------
        /// <summary> RestaurantService Constructor </summary>
        ///
        /// <param name="restaurantRepository"> An instance of a restaurant repository. The framework uses
        ///                                     dependency injection to supply the instance bewcause
        ///                                     it was registered in the ConfigureServices method of the
        ///                                     Startup class.</param>
        ///-------------------------------------------------------------------------------------------------

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            this._restaurantRepository = restaurantRepository;
        }

        #region IRestaurantService Members

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets all restaurants. </summary>
        ///
        /// <param name="name"> (Optional) The name of a single restaurant. </param>
        /// <returns>   all restaurants as a List of Restarant objects. </returns>
        ///-------------------------------------------------------------------------------------------------

        public List<Restaurant> GetAllRestaurants(string name = null)
        {
            List<Restaurant> list;

            if (string.IsNullOrEmpty(name))
            {
                list = _restaurantRepository.GetAll().ToList();
            }

            else
            {
                list = _restaurantRepository.GetAll().Where(x => x.Name == name).ToList();
            }

            return list;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets all the restaurants by city. </summary>
        ///
        /// <param name="city"> The city that we are trying to find restaurants in. </param>
        ///
        /// <returns>   The restaurants by city. </returns>
        /// 
        ///  <remarks>   This could be more robust.  Searching for a city really requires geodata to do it right.
        ///              See my design decisions for more details.</remarks>
        ///
        ///-------------------------------------------------------------------------------------------------

        public List<Restaurant> GetRestaurantsByCity(string city)
        {
            List<Restaurant> restaurant = _restaurantRepository.GetRestaurantsByCity(city).ToList();
            return restaurant;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets restaurant by GUID identifier. </summary>
        ///
        /// <param name="id">   A Guid identifier. </param>
        ///
        /// <returns>   The restaurant identified by the argument. </returns>
        /// 
        /// <remarks> An endpoint using this method is unused in this exercise.</remarks>
        ///-------------------------------------------------------------------------------------------------

        public Restaurant GetRestaurantById(Guid id)
        {
            var restaurant = _restaurantRepository.GetById(id);
            return restaurant;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates a restaurant object and adds it to the data store. </summary>
        ///
        /// <param name="restaurant">   The restaurant object from which the restaurant will be created.</param>
        ///-------------------------------------------------------------------------------------------------

        public void CreateRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Add(restaurant);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Updates the restaurant in the data store described by the restaurant object. </summary>
        ///
        /// <param name="restaurant">   The restaurant object from which the restaurant will be updated. </param>
        ///-------------------------------------------------------------------------------------------------

        public void UpdateRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Update(restaurant);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Deletes the restaurant in the data store  described by the restaurant object. </summary>
        ///
        /// <param name="restaurant">   The restaurant object that will be deleted from a data store. </param>
        ///-------------------------------------------------------------------------------------------------

        public void DeleteRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Delete(restaurant);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves the changes that we make when creating or updating a database. </summary>
        ///-------------------------------------------------------------------------------------------------

        public void SaveChanges()
        {
            _restaurantRepository.Save();
        }

        #endregion
    }

}