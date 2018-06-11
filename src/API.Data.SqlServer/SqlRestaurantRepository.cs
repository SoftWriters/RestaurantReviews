/******************************************************************************
 * Name: SqlRestaurantRepository.cs
 * Purpose: Restaurant Repository that uses EF to perform Repo operations
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Data.SqlServer.DataModel;

namespace RestaurantReviews.API.Data.SqlServer
{
    public class SqlRestaurantRepository : IRestaurantRepository
    {
        public RestaurantModelList GetRestaurants(string city)
        {
            RestaurantModelList ret = new RestaurantModelList();
            ret.RestaurantList = new List<RestaurantModelDTO>();

            using (var db = new RestaurantReviewsContext())
            {
                var restaurants = from r in db.TblRestaurant
                                  where r.City == city
                                  select r;

                foreach (TblRestaurant restaurant in restaurants)
                {
                    ret.RestaurantList.Add(ModelFactory.Create(restaurant));
                }
            }

            return ret;
        }

        public RestaurantModelDTO GetRestaurantById(int id)
        {
            RestaurantModelDTO ret = null;

            using (var db = new RestaurantReviewsContext())
            {
                var restaurant = db.TblRestaurant.Find(id);

                if (restaurant != null) ret = ModelFactory.Create(restaurant);
            }

            return ret;
        }

        public bool CheckRestaurantExists(RestaurantModelDTO restaurant)
        {
            using (var db = new RestaurantReviewsContext())
            {
                var restaurantTable = db.TblRestaurant.Find(restaurant.Name, restaurant.City);

                if (restaurant != null) return true;
            }

            return false;
        }

        public RestaurantModelDTO AddRestaurant(RestaurantModelDTO newRestaurant)
        {
            RestaurantModelDTO ret = null;

            using (var db = new RestaurantReviewsContext())
            {
                TblRestaurant restaurant = ModelFactory.Create(newRestaurant);
                db.TblRestaurant.Add(restaurant);
                db.SaveChanges();
                ret = ModelFactory.Create(restaurant);
            }

            return ret;
        }
    }
}
