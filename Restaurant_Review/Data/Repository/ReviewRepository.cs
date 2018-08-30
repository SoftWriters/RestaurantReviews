using Dapper;
using Restaurant_Review;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Restaurant_Review.ViewModels;

namespace Restaurant_Review.Data.Repository
{
    /// <summary>
    /// review repository class
    /// </summary>
    public class ReviewRepository: IRepository<Reviews>
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
        public ReviewRepository(string conn, string username)
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
                return  dbConnection.Query<string>(uQ, new { UserName = _userName }).FirstOrDefault();
            }
        }

        /// <summary>
        /// add new review
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public int Add(Reviews res)
        {
            int result = 0;
            try
            {
                using (var dbConnection = Connection)
                {
                    const string sQuery = "INSERT INTO Reviews (ReviewText, IDRestaurant, IDUser, DateCreated)"
                                          + " VALUES(@ReviewText, @IDRestaurant, @IDUser, @DateCreated)";
                    
                    dbConnection.Execute(sQuery, new Reviews
                    {
                        ReviewText = res.ReviewText,
                        IDRestaurant = res.IDRestaurant,
                        IDUser = res.IDUser ?? _uid,
                        DateCreated = res.DateCreated
                    });
                    result = dbConnection.Query<int>("Select CAST(IDENT_CURRENT('reviews') as int)").SingleOrDefault();
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
        /// delete review by id
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
                    const string sQuery = "DELETE FROM Reviews"
                                          + " WHERE IDReview = @Id";
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
        /// get all reviews
        /// </summary>
        /// <returns></returns>
        public List<Reviews> GetAll()
        {
            var list = new List<Reviews>();
            try
            {
                using (var dbConnection = Connection)
                {
                    dbConnection.Open();
                    list = dbConnection.Query<Reviews>("SELECT * FROM Reviews").ToList();
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
        /// returns a list of reviews and users
        /// </summary>
        /// <returns></returns>
        public List<ReviewViewModel> GetReviewsByUsers()
        {
            var list = new List<ReviewViewModel>();
            try
            {
                using (var dbConnection = Connection)
                {
                    dbConnection.Open();
                    list = dbConnection.Query<ReviewViewModel>(@"select r.IDRestaurant,
                    r.RestaurantName,
                    r.RestaurantDescription,
                    v.IDReview,
                    v.ReviewText,
                    v.IDUser,
                    u.UserName,
                    r.Line1,
                    r.Line2,
                    r.Line3,
                    r.City,
                    r.StateCode,
                    r.ZipCode,
                    r.Country
                        from Reviews v
                    inner 
                        join Restaurant r on v.IDRestaurant = r.IDRestaurant
                    inner 
                        join AspNetUsers u on v.IDUser = u.ID    
                    order by r.RestaurantName").ToList();
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
        /// get all reviews by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IList<ReviewViewModel> GetReviewsByUserName(string userName)
        {
            IList<ReviewViewModel> list = new List<ReviewViewModel>();
            try
            {
                using (var dbConnection = Connection)
                {
                    var qry = "%" + userName + "%";
                    dbConnection.Open();
                    list = dbConnection.Query<ReviewViewModel>(@"select r.IDRestaurant,
                    r.RestaurantName,
                    r.RestaurantDescription,
                    v.IDReview,
                    v.ReviewText,
                    v.IDUser,
                    u.UserName,
                    r.Line1,
                    r.Line2,
                    r.Line3,
                    r.City,
                    r.StateCode,
                    r.ZipCode,
                    r.Country
                        from Reviews v
                    inner 
                        join Restaurant r on v.IDRestaurant = r.IDRestaurant
                    inner 
                        join AspNetUsers u on v.IDUser = u.ID    
                    where u.UserName like @val
                    order by r.RestaurantName", new {val = qry}).ToList();
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
       /// get a review by id
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public Reviews GetById(int id)
        {
            try
            {
                using (var dbConnection = Connection)
                {
                    const string sQuery = "SELECT * FROM Reviews"
                                          + " WHERE IDReview = @Id";
                    dbConnection.Open();
                    return dbConnection.Query<Reviews>(sQuery, new { Id = id }).FirstOrDefault();
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
        /// update a review
        /// </summary>
        /// <param name="prod"></param>
        /// <returns></returns>
        public bool Update(Reviews prod)
        {
            var bSuccess = true;
            try
            {
                using (var dbConnection = Connection)
                {
                    const string sQuery = "UPDATE Reviews SET ReviewText = @ReviewText,"
                                          + " IDRestaurant = @IDRestaurant"
                                          + " WHERE IDReview = @IDReview";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new Reviews
                    {
                        IDReview = prod.IDReview, ReviewText = prod.ReviewText, 
                        IDRestaurant = prod.IDRestaurant
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