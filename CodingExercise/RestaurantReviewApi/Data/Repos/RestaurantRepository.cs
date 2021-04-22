using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Data
{
    public class RestaurantRepository : BaseRepository, IRestaurantRepository
    {
        public Restaurant ReadRestaurant(int id)
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                Restaurant restaurant = conn.Query<Restaurant>(
                        "SELECT rs.Id, rs.Name, rs.Description, rs.CityId, ct.Name as CityName " +
                        "FROM Restaurant rs " +
                        "   INNER JOIN City ct on rs.CityId = ct.Id " +
                        "WHERE rs.Id= @id", 
                        new { id }).FirstOrDefault();

                restaurant.Reviews = conn.Query<Review>(
                    "SELECT rv.Id, rv.RestaurantId, rs.Name as RestaurantName, rv.UserId, ur.Name as UserName, rv.DateSubmitted, rv.Text, rv.Rating " +
                    "FROM Review rv " +
                    "   INNER JOIN Restaurant rs on rv.RestaurantId = rs.Id " +
                    "   INNER JOIN User ur on rv.UserId = ur.Id " +
                    "WHERE RestaurantId = @id"
                    , new { id }).ToList();

                return restaurant;
            }
        }

        public IList<Restaurant> ReadRestaurantsByCity(int cityId)
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                string query = "SELECT rs.Id, rs.Name, rs.Description, rs.CityId, ct.Name as CityName " +
                        "FROM Restaurant rs " +
                        "   INNER JOIN City ct on rs.CityId = ct.Id " +
                        "WHERE rs.CityId= @cityId";
                IList<Restaurant> restaurants = conn.Query<Restaurant>(query, new { cityId }).ToList();

                return restaurants;
            }
        }

        public IList<Restaurant> ReadAllRestaurants()
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                string query = "SELECT rs.Id, rs.Name, rs.Description, rs.CityId, ct.Name as CityName " +
                        "FROM Restaurant rs " +
                        "   INNER JOIN City ct on rs.CityId = ct.Id";
                IList<Restaurant> restaurants = conn.Query<Restaurant>(query).ToList();

                return restaurants;
            }
        }

        public void CreateRestaurant(Restaurant restaurant)
        {
            if (!File.Exists(ResturantReviewDbFile))
            {
                CreateDatabase();
            }

            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                restaurant.Id = conn.Query<int>(
                    $"INSERT INTO Restaurant (Name, Description, CityId) " +
                    $"VALUES (@Name, @Description, @CityId); " +
                    $"SELECT last_insert_rowid()", restaurant
                ).First();
            }
        }
    }
}
