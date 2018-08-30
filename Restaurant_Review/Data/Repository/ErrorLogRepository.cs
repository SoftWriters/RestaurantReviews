using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Restaurant_Review;

namespace Restaurant_Review.Data.Repository
{
    public class ErrorLogRepository : IRepository<ErrorLog>
    {
        private readonly string _connString;

        public ErrorLogRepository(string conn)
        {
            _connString = conn;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connString);
            }
        }

        public int Add(ErrorLog res)
        {
            var result = 0;
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    var sQuery = "INSERT INTO ErrorLog (UserName, ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)"
                                    + " VALUES(@UserName, @ErrorNumber, @ErrorSeverity, @ErrorState, @ErrorProcedure, @ErrorLine, @ErrorMessage)";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, res);
                    result = dbConnection.Query<int>("SELECT @@IDENTITY").Single();
                }
            }
            catch (Exception e)
            {
                //todo log
            }
            return result;
        }

        public bool Delete(int id)
        {
            var bSuccess = true;
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    var sQuery = "DELETE FROM ErrorLog"
                                 + " WHERE ErrorLogID = @Id";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Id = id });
                }
            }
            catch (Exception e)
            {
                bSuccess = false;
            }
            return bSuccess;
        }

        public List<ErrorLog> GetAll()
        {
            List<ErrorLog> list = new List<ErrorLog>();
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                list = dbConnection.Query<ErrorLog>("SELECT * FROM ErrorLog").ToList();
            }
            return list;
        }

        public ErrorLog GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var sQuery = "SELECT * FROM ErrorLog"
                                + " WHERE ErrorLogID = @Id";
                dbConnection.Open();
                return dbConnection.Query<ErrorLog>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }

        public bool Update(ErrorLog prod)
        {
            var bSuccess = true;
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    string sQuery = "UPDATE ErrorLog SET ErrorTime = @ErrorTime,"
                                    + " UserName = @UserName, ErrorNumber = @ErrorNumber, ErrorSeverity = @ErrorSeverity, ErrorState = @ErrorState, ErrorProcedure = @ErrorProcedure, ErrorLine = @ErrorLine, ErrorMessage = @ErrorMessage"
                                    + " WHERE ErrorLogId = @Id";
                    dbConnection.Open();
                    dbConnection.Query(sQuery, prod);
                }
            }
            catch (Exception e)
            {
                //todo log
                bSuccess = false;
            }
            return bSuccess;
        }


    }
}
