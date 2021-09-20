using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace RestaurantReviews.DataAccess
{
    public class DBCaller
    {
        public delegate T Callback<T>(SqlCommand command, SqlParameter returnParam);

        public static string SpecificConnectionString = null;
        private static string ConnectionStringName = ConfigurationManager.AppSettings["ConnectionStringName"];
        private static string ConnectionStringName_Test = ConfigurationManager.AppSettings["ConnectionStringName_Test"];
        public static bool IsTest = false;

        private static string GetConnectionString()
        {
            if (!string.IsNullOrEmpty(SpecificConnectionString))
                return SpecificConnectionString;

            return ConfigurationManager.ConnectionStrings[IsTest ? ConnectionStringName_Test : ConnectionStringName].ConnectionString;
        }

        public static int Call(string commandText, IEnumerable<SqlParameter> paramList, CommandType commandType = CommandType.StoredProcedure)
        {
            return CallGeneric<int>(commandText, commandType, paramList, (command, returnParam) =>
            {
                command.ExecuteNonQuery();
                return (int)returnParam.Value;
            });
        }

        public static List<T> CreateModelList<T>(string commandText, IEnumerable<SqlParameter> paramList, CommandType commandType = CommandType.StoredProcedure) where T : new()
        {
            return CallGeneric<List<T>>(commandText, commandType, paramList, (command, returnParam) =>
            {
                List<T> modelList = null;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    modelList = CreateModelList<T>(reader);
                }

                return modelList;
            });
        }

        public static T CallGeneric<T>(string commandText, CommandType commandType, IEnumerable<SqlParameter> paramList, Callback<T> callback = null)
        {
            SqlParameter returnParam = GetReturnValueParameter();
            T result = default(T);

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand command = GetSqlCommand(commandText, commandType, paramList, conn))
                {
                    command.Parameters.Add(returnParam);

                    if (callback != null)
                        result = callback(command, returnParam);

                    conn.Close();
                }
            }

            return result;
        }

        public static SqlParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        public static List<SqlParameter> CreateParameterList(params object[] values)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            string paramName = "";

            for (int i = 0; i < values.Length; i++)
            {
                if (i % 2 == 0)
                    paramName = (string)values[i];
                else
                    paramList.Add(CreateParameter(paramName, values[i]));
            }

            return paramList;
        }

        private static List<T> CreateModelList<T>(SqlDataReader reader) where T : new()
        {
            List<T> modelList = new List<T>();

            while (reader.Read())
            {
                modelList.Add(CreateModel<T>(reader));
            }

            return modelList;
        }

        public static T CreateModel<T>(SqlDataReader reader) where T : new()
        {
            T model = new T();

            Type inputType = typeof(T);
            if (inputType.IsPrimitive)
            {
                object readerValue = reader[0];
                readerValue = (readerValue == DBNull.Value) ? null : readerValue;
                model = (T)readerValue;
            }

            PropertyInfo[] properties = inputType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (HasColumn(reader, property.Name))
                {
                    object readerValue = reader[property.Name];
                    readerValue = (readerValue == DBNull.Value) ? null : readerValue;
                    property.SetValue(model, readerValue, null);
                }
            }

            return model;
        }

        private static SqlCommand GetSqlCommand(string commandText, CommandType commandType, IEnumerable<SqlParameter> paramList, SqlConnection conn)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = commandType;
            command.CommandText = commandText;

            if (paramList != null)
            {
                foreach (SqlParameter param in paramList)
                {
                    command.Parameters.Add(param);
                }
            }

            return command;
        }

        private static SqlParameter GetReturnValueParameter()
        {
            SqlParameter param = new SqlParameter("@RETURN_VALUE", 0);
            param.SqlDbType = SqlDbType.Int;
            param.Direction = ParameterDirection.ReturnValue;
            return param;
        }

        private static bool HasColumn(SqlDataReader reader, string columnName)
        {
            foreach (DataRow row in reader.GetSchemaTable().Rows)
            {
                if (row["ColumnName"].ToString() == columnName)
                    return true;
            }
            return false;
        }
    }
}
