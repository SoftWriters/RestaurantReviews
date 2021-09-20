using RestaurantReviews.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantReviews.DataAccess
{
    public static class UserManager
    {
        public static List<UserModel> GetUsers(int ID)
        {
            return DBCaller.CreateModelList<UserModel>("proc_GetUser", DBCaller.CreateParameterList("@ID", ID));
        }

        public static List<UserModel> GetAllUsers()
        {
            return GetUsers(0);
        }

        public static UserModel GetUser(int ID)
        {
            return GetUsers(ID)[0];
        }

        public static int InsertUser(string username, string firstName, string lastName, string password)
        {
            return UpdateUser(0, username, firstName, lastName, password);
        }

        public static int UpdateUser(UserModel user)
        {
            return UpdateUser(user.ID, user.Username, user.FirstName, user.LastName, user.Password);
        }

        public static int UpdateUser(int ID, string username, string firstName, string lastName, string password)
        {
            password = string.IsNullOrWhiteSpace(password) ? null : password;
            return DBCaller.Call("proc_UpdateUser", DBCaller.CreateParameterList("@ID", ID, "@Username", username, "@FirstName", firstName, "@LastName", lastName, "@Password", password));
        }

        public static KeyValuePair<int, UserModel> UserLogin(string username, string password)
        {
            return DBCaller.CallGeneric<KeyValuePair<int, UserModel>>("proc_UserLogin", CommandType.StoredProcedure, DBCaller.CreateParameterList("@Username", username, "@Password", password), 
                (command, returnParam) =>
            {
                UserModel model = default(UserModel);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        model = DBCaller.CreateModel<UserModel>(reader);
                }

                return new KeyValuePair<int, UserModel>((int)returnParam.Value, model);
            });
        }
    }
}
