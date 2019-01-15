using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;

namespace RestaurantReviews.Data.Tests
{
    [TestClass]
    public class DataManagerTestBase
    {
        private TransactionScope _transactionScope;
        public DbContext DbContext;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString;
            DbContext = new DbContext() { ConnnectionString = connString };
            _transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { Timeout = new TimeSpan(0, 0, 20) });
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            //Transaction.Current.Rollback();
            _transactionScope.Dispose();
        }

        protected void ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] commandParameters)
        {
            using (var connection = new SqlConnection(DbContext.ConnnectionString))
            using (var command = new SqlCommand(commandText, connection))
            {
                command.CommandType = commandType;
                command.Parameters.AddRange(commandParameters);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        protected IDataReader ExecuteReader(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            //leave open until reader is disposed
            var connection = new SqlConnection(DbContext.ConnnectionString);
            using (var command = new SqlCommand(commandText, connection))
            {
                connection.Open();
                command.CommandType = commandType;
                command.Parameters.AddRange(parameters);
                
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                
                return reader;
            }
        }

        protected T ExecuteScalar<T>(string commandText) where T : IConvertible 
        {
            using (var connection = new SqlConnection(DbContext.ConnnectionString))
            using (var command = new SqlCommand(commandText, connection))
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                var result = command.ExecuteScalar();
                
                var val =  (T)Convert.ChangeType(result, typeof(T));
                connection.Close();
                return val;
            }
        }


    }
}
