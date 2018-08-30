using Dapper;
using Restaurant_Review.ViewModels;
using Restaurant_Review;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Restaurant_Review.Data.Repository
{
    /// <summary>
    /// restaurant repository class
    /// </summary>
    public class RestaurantRepository: IRepository<Restaurant>
    {
        private readonly string _connString;
        private readonly string _userName;
        private readonly string _uid;
        private readonly ErrorLogRepository _errRepository;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="username"></param>
        public RestaurantRepository(string conn, string username)
        {
            _connString = conn;
            _userName = username;
            _errRepository = new ErrorLogRepository(conn);

            _uid = GetUserId(_userName);

        }
        /// <summary>
        /// Connection property
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connString);
            }
        }
        /// <summary>
        /// returns UID
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GetUserId(string username)
        {
            using (var dbConnection = Connection)
            {
                const string uQ = "SELECT Id FROM AspNetUsers"
                                  + " WHERE UserName = @username";
                dbConnection.Open();
                return dbConnection.Query<string>(uQ, new { UserName = _userName }).FirstOrDefault();
            }
        }
        /// <summary>
        /// add new restaurant
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public int Add(Restaurant res)
        {
            var result = 0;
            try
            {
                using (var dbConnection = Connection)
                {
                    const string sQuery = "INSERT INTO Restaurant (RestaurantName, RestaurantDescription, DateCreated, IDUserCreated, Line1, Line2, Line3, City, ZipCode, StateCode, Country)"
                                          + " VALUES(@RestaurantName, @RestaurantDescription, @DateCreated, @IDUserCreated, @Line1, @Line2, @Line3, @City, @ZipCode, @StateCode, @Country)";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new Restaurant
                    {
                        RestaurantName = res.RestaurantName,
                        RestaurantDescription = res.RestaurantDescription,
                        DateCreated = res.DateCreated,
                        IDUserCreated = res.IDUserCreated ?? _uid,
                        Line1 = res.Line1,
                        Line2 = res.Line2,
                        Line3 = res.Line3,
                        City = res.City,
                        StateCode = res.StateCode,
                        Country = res.Country
                    });
                    result = dbConnection.Query<int>("Select CAST(IDENT_CURRENT('restaurant') as int)").SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                var er = new ErrorLog
                {
                    UserName = _userName,
                    ErrorMessage = e.Message
                };
                _errRepository.Add(er);
            }
            return result;
        }
        /// <summary>
        /// delete restaurant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var bSuccess = true;
            try
            {
                using (var dbConnection = Connection)
                {
                    const string sQuery = "DELETE FROM Restaurant"
                                          + " WHERE IDRestaurant = @Id";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new {Id = id});
                }
            }
            catch (Exception e)
            {
                bSuccess = false;
                var er = new ErrorLog
                {
                    UserName = _userName,
                    ErrorMessage = e.Message
                };
                _errRepository.Add(er);
            }
            return bSuccess;
        }
        /// <summary>
        /// Get all restaurants
        /// </summary>
        /// <returns></returns>
        public List<Restaurant> GetAll()
        {
            var list = new List<Restaurant>();
            try
            {
                using (var dbConnection = Connection)
                {
                    dbConnection.Open();
                    list = dbConnection.Query<Restaurant>("SELECT * FROM Restaurant Order By RestaurantName").ToList();
                }
            }
            catch (Exception e)
            {
                //todo
                var er = new ErrorLog
                {
                    UserName = _userName,
                    ErrorMessage = e.Message
                };
                _errRepository.Add(er);
            }
            return list;
        }

        /// <summary>
        /// Return all restaurants with address
        /// </summary>
        /// <returns></returns>
        public IList<RestaurantViewModel> GetAllWithAddress()
        {
            IList<RestaurantViewModel> list = new List<RestaurantViewModel>();
            try
            {
                using (var dbConnection = Connection)
                {
                    dbConnection.Open();
                    list = dbConnection.Query<RestaurantViewModel>(@"select IDRestaurant,
                    RestaurantName,
                    RestaurantDescription,
                    Line1,
                    Line2,
                    Line3,
                    City,
                    StateCode,
                    Country,
                    ZipCode
                    from restaurant
                    order by Restaurantname").ToList();
                }
            }
            catch (Exception e)
            {
                var er = new ErrorLog
                {
                    UserName = _userName,
                    ErrorMessage = e.Message
                };
                _errRepository.Add(er);
            }
            return list;
        }
        /// <summary>
        /// Get restaurant by City
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public List<Restaurant> GetByCity(string city)
        {
            var list = new List<Restaurant>();
            try
            {
                using (var dbConnection = Connection)
                {
                    var qry = "%" + city + "%";
                    dbConnection.Open();
                    list = dbConnection.Query<Restaurant>(@"select IDRestaurant,
                    RestaurantName,
                    RestaurantDescription,
                    Line1,
                    Line2,
                    Line3,
                    City,
                    StateCode,
                    ZipCode,
                    Country
                    from Restaurant 
                    where City like @val
                    order by RestaurantName", new {val = qry}).ToList();
                }
            }
            catch (Exception e)
            {
                var er = new ErrorLog
                {
                    UserName = _userName,
                    ErrorMessage = e.Message
                };
                _errRepository.Add(er);
            }
            return list;
        }
        /// <summary>
        /// get restaurant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Restaurant GetById(int id)
        {
            try
            {
                using (var dbConnection = Connection)
                {
                    const string sQuery = "SELECT * FROM Restaurant"
                                          + " WHERE IDRestaurant = @Id";
                    dbConnection.Open();
                    return dbConnection.Query<Restaurant>(sQuery, new { Id = id }).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                var er = new ErrorLog
                {
                    UserName = _userName,
                    ErrorMessage = e.Message
                };
                _errRepository.Add(er);
                return null;
            }
        }
        /// <summary>
        /// update restaurant
        /// </summary>
        /// <param name="prod"></param>
        /// <returns></returns>
        public bool Update(Restaurant prod)
        {
            var bSuccess = true;
            try
            {
                using (var dbConnection = Connection)
                {

                    const string sQuery = "UPDATE Restaurant SET RestaurantName = @RestaurantName,"
                                          +" RestaurantDescription = @RestaurantDescription, DateUpdated = @DateUpdated, IDUserUpdated = @IDUserUpdated," 
                                          +" Line1 = @Line1, Line2 = @Line2, Line3 = @Line3, City=@City, ZipCode = @ZipCode, Country=@Country, StateCode = @StateCode"
                                          +" WHERE IDRestaurant = @IdRestaurant";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new Restaurant {IDRestaurant = prod.IDRestaurant,
                        RestaurantName = prod.RestaurantName, RestaurantDescription = prod.RestaurantDescription,
                        DateUpdated = DateTime.UtcNow, IDUserCreated = prod.IDUserCreated ?? _uid,
                        Line1 = prod.Line1, Line2 = prod.Line2, Line3 = prod.Line3, City = prod.City, StateCode = prod.StateCode,
                        Country = prod.Country
                    });
                }
            }
            catch (Exception e)
            {
                var er = new ErrorLog
                {
                    UserName = _userName,
                    ErrorMessage = e.Message
                };
                _errRepository.Add(er);
                bSuccess = false;
            }
            return bSuccess;
        }

     
    }
}