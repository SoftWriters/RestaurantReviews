using RestaurantReview.BusinessLogic.Models;
using RestaurantReview.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.BusinessLogic.Controllers
{
    public class RestaurantLogic : Logic
    {
        private RestaurantDBContext _dbContext;

        public RestaurantLogic()
        {
            _dbContext = new RestaurantDBContext();
        }

        public bool TryAddRestaurant(RestaurantContext restaurant, out HttpStatusCode suggestedStatusCode, out string errorMessage, out Restaurant result)
        {
            suggestedStatusCode = HttpStatusCode.OK;
            errorMessage = "";
            result = null;

            try
            {
                RestaurantContext resultContext = _dbContext.Restaurants.Add(restaurant);
                _dbContext.SaveChanges();

                result = new Restaurant(resultContext);
                return true;
            }

            catch (Exception ex)
            {
                return TotalFailureMessage(ex, out suggestedStatusCode, out errorMessage);
            }
        }

        public bool TryGetRestaurant(int restaurantID, out HttpStatusCode suggestedStatusCode, out string errorMessage, out Restaurant result)
        {
            suggestedStatusCode = HttpStatusCode.OK;
            errorMessage = "";
            result = null;

            try
            {
                RestaurantContext restaurantContext = _dbContext.Restaurants.Find(restaurantID);

                if (restaurantContext != null)
                {
                    result = new Restaurant(restaurantContext);
                }

                else
                {
                    suggestedStatusCode = HttpStatusCode.BadRequest;
                    errorMessage = $"Invalid restaurant ID: {restaurantID}. Record not found.";
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                return TotalFailureMessage(ex, out suggestedStatusCode, out errorMessage);
            }
        }

        public bool TryGetRestaurant(string restaurantName, out HttpStatusCode suggestedStatusCode, out string errorMessage, out Restaurant restaurant)
        {
            suggestedStatusCode = HttpStatusCode.OK;
            errorMessage = "";
            restaurant = null;

            try
            {
                restaurantName = CleanRestaurantName(restaurantName);

                if (_dbContext.Restaurants.Any(x => CleanRestaurantName(x.name) == restaurantName))
                {
                    List<RestaurantContext> restaurants = _dbContext.Restaurants.Where(x => CleanRestaurantName(x.name) == restaurantName).ToList();
                    if (restaurants.Count > 1)
                    {
                        suggestedStatusCode = HttpStatusCode.InternalServerError;
                        errorMessage = "Multiple records for unique name value";
                        return false;
                    }

                    else
                    {
                        restaurant = new Restaurant(restaurants[0]);
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                return TotalFailureMessage(ex, out suggestedStatusCode, out errorMessage);
            }
        }

        public bool TryGetRestaurants(out HttpStatusCode suggestedStatusCode, out string errorMessage, out List<Restaurant> restaurants)
        {
            suggestedStatusCode = HttpStatusCode.OK;
            errorMessage = "";
            restaurants = null;

            try
            {
                restaurants = new List<Restaurant>();

                foreach (RestaurantContext restaurantContext in _dbContext.Restaurants)
                {
                    Restaurant restaurant = new Restaurant(restaurantContext);
                    restaurants.Add(restaurant);
                }

                return true;
            }

            catch (Exception ex)
            {
                return TotalFailureMessage(ex, out suggestedStatusCode, out errorMessage);
            }
        }

        public bool TryUpdateRestaurant(int restaurantID, RestaurantContext restaurant, out HttpStatusCode suggestedStatusCode, out string errorMessage, out Restaurant result)
        {
            suggestedStatusCode = HttpStatusCode.OK;
            errorMessage = "";
            result = null;

            try
            {
                RestaurantContext restaurantContext = _dbContext.Restaurants.Find(restaurantID);

                if (restaurantContext != null)
                {
                    restaurantContext.name = restaurant.name;
                    restaurantContext.streetAddress = restaurant.streetAddress;
                    restaurantContext.zipcode = restaurant.zipcode;
                    restaurantContext.city = restaurant.city;
                    restaurantContext.state = restaurant.state;
                    restaurantContext.country = restaurant.country;
                    restaurantContext.thumbnailBase64 = restaurant.thumbnailBase64;
                    result = new Restaurant(restaurantContext);
                    _dbContext.SaveChanges();
                }

                else
                {
                    suggestedStatusCode = HttpStatusCode.BadRequest;
                    errorMessage = $"Invalid restaurant ID: {restaurantID}. Record not found.";
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                return TotalFailureMessage(ex, out suggestedStatusCode, out errorMessage);
            }
        }

        public bool TryDeleteRestaurant(int restaurantID, out HttpStatusCode suggestedStatusCode, out string errorMessage)
        {
            suggestedStatusCode = HttpStatusCode.OK;
            errorMessage = "";

            try
            {
                RestaurantContext restaurantContext = _dbContext.Restaurants.Find(restaurantID);

                if (restaurantContext != null)
                {
                    _dbContext.Restaurants.Remove(restaurantContext);
                    _dbContext.SaveChanges();
                }

                else
                {
                    suggestedStatusCode = HttpStatusCode.BadRequest;
                    errorMessage = $"Invalid restaurant ID: {restaurantID}. Record not found.";
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                return TotalFailureMessage(ex, out suggestedStatusCode, out errorMessage);
            }
        }
    }
}
